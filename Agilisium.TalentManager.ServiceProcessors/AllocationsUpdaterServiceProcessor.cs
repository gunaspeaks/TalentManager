using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Repository.Abstract;
using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.ServerUtilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Agilisium.TalentManager.ServiceProcessors
{
    public class AllocationsUpdaterServiceProcessor
    {
        private readonly AllocationRepository allocationRepo;
        private readonly EmployeeRepository employeeRepo;
        private readonly NotificationsTrackerRepository trackerRepo;
        private readonly ProjectRepository projectRepo;
        private readonly PracticeRepository practiceRepository;

        private readonly string emailClientIP;
        private readonly string ownerEmailID;
        private readonly string outlookPwd;

        public AllocationsUpdaterServiceProcessor()
        {
            allocationRepo = new AllocationRepository();
            employeeRepo = new EmployeeRepository();
            projectRepo = new ProjectRepository();
            trackerRepo = new NotificationsTrackerRepository();
            practiceRepository = new PracticeRepository();
            emailClientIP = ProcessorHelper.GetSettingsValue(ProcessorHelper.EMAIL_PROXY_SERVER);
            ownerEmailID = ProcessorHelper.GetSettingsValue(ProcessorHelper.CONTRACTOR_REQ_EMAIL_OWNER);
            outlookPwd = ProcessorHelper.GetSettingsValue(ProcessorHelper.EMAIL_OWNERS_PASSWORD);
        }

        public int ProcessAllocations()
        {
            int newAllocations = 0;
            List<ProjectAllocationDto> activeAllocations = allocationRepo.GetAllRecords().ToList();
            List<ProjectAllocationDto> allocationsToProcess = activeAllocations.Where(a => a.AllocationEndDate.Subtract(DateTime.Now).TotalDays <= 30
            && a.ProjectName.ToLower().Contains("bench") == false).ToList();

            foreach (ProjectAllocationDto allocation in allocationsToProcess)
            {
                double daysDifference = allocation.AllocationEndDate.Subtract(DateTime.Now).TotalDays;

                if ((daysDifference > 29 && daysDifference < 31)
                    || (daysDifference > 14 && daysDifference < 16)
                    || (daysDifference > 4 && daysDifference < 6)
                    || (daysDifference > 0 && daysDifference < 2))
                {
                    SendAllocationEmail(allocation);
                }
                else if (daysDifference < 0 && daysDifference > -2)
                {
                    MoveResourceToBenchProject(allocation);
                }
                else
                {
                    continue;
                }
            }

            return newAllocations;
        }

        private void SendAllocationEmail(ProjectAllocationDto allocation)
        {
            EmployeeDto emp = employeeRepo.GetByID(allocation.EmployeeID);
            ProjectDto proj = projectRepo.GetByID(allocation.ProjectID);
            EmployeeDto pm = employeeRepo.GetByID(proj.ProjectManagerID);
            EmployeeDto rm = emp.ReportingManagerID.HasValue ? employeeRepo.GetByID(emp.ReportingManagerID.Value) : null;


            StringBuilder toEmailID = new StringBuilder();
            if (!string.IsNullOrEmpty(pm?.EmailID))
            {
                toEmailID.Append(pm.EmailID);
            }
            else
            {
                toEmailID.Append("satish.srinivasan@agilisium.com");
            }

            StringBuilder bccEmailIDs = new StringBuilder();
            if (!string.IsNullOrEmpty(emp?.EmailID))
            {
                bccEmailIDs.Append(emp.EmailID + ";");
            }

            if (!string.IsNullOrEmpty(rm?.EmailID))
            {
                bccEmailIDs.Append(rm.EmailID + ";");
            }

            bccEmailIDs.Append("satish.srinivasan@agilisium.com");

            string emailSubject = "RMT Alert - Resource Allocation Notification";
            EmailHandler emailHandler = new EmailHandler(ownerEmailID, outlookPwd);
            string emailContent = GenerateAllocationEndingEmailContent(allocation);

            emailHandler.SendEmail(emailClientIP, toEmailID.ToString(), emailSubject, emailContent, bccEmailIDs.ToString());
        }

        private void MoveResourceToBenchProject(ProjectAllocationDto allocation)
        {
            EmployeeDto emp = employeeRepo.GetByID(allocation.EmployeeID);
            ProjectDto benchProject = projectRepo.GetBenchProjectByPractice(emp.PracticeID);
            if (benchProject == null)
            {
                SendAllocationFailureNotification(allocation);
                return;
            }

            ProjectAllocationDto newAllocation = new ProjectAllocationDto
            {
                AllocationEndDate = benchProject.EndDate,
                AllocationStartDate = DateTime.Now,
                AllocationTypeID = (int)AllocationType.NonCommittedBuffer,
                EmployeeID = allocation.EmployeeID,
                PercentageOfAllocation = 100,
                ProjectID = benchProject.ProjectID,
                Remarks = "Automatically moved to Bench by RMT"
            };
            allocationRepo.Add(newAllocation);
            SendNewAllocationNotification(allocation, benchProject, emp);
        }

        private void SendAllocationFailureNotification(ProjectAllocationDto allocation)
        {
            EmployeeDto emp = employeeRepo.GetByID(allocation.EmployeeID);
            ProjectDto proj = projectRepo.GetByID(allocation.ProjectID);
            EmployeeDto pm = employeeRepo.GetByID(proj.ProjectManagerID);
            EmployeeDto rm = emp.ReportingManagerID.HasValue ? employeeRepo.GetByID(emp.ReportingManagerID.Value) : null;


            StringBuilder toEmailID = new StringBuilder();
            if (!string.IsNullOrEmpty(pm?.EmailID))
            {
                toEmailID.Append(pm.EmailID);
            }
            else
            {
                toEmailID.Append("satish.srinivasan@agilisium.com");
            }

            StringBuilder bccEmailIDs = new StringBuilder();
            if (!string.IsNullOrEmpty(emp?.EmailID))
            {
                bccEmailIDs.Append(emp.EmailID + ";");
            }

            if (!string.IsNullOrEmpty(rm?.EmailID))
            {
                bccEmailIDs.Append(rm.EmailID + ";");
            }

            bccEmailIDs.Append("satish.srinivasan@agilisium.com");

            string emailSubject = "Automated Resource Allocation Failure";
            EmailHandler emailHandler = new EmailHandler(ownerEmailID, outlookPwd);
            string emailContent = GenerateAllocationFailureEmailContent(allocation);

            emailHandler.SendEmail(emailClientIP, toEmailID.ToString(), emailSubject, emailContent, bccEmailIDs.ToString());
        }

        private void SendNewAllocationNotification(ProjectAllocationDto allocation, ProjectDto benchProject, EmployeeDto employee)
        {
            EmployeeDto pm = employeeRepo.GetByID(benchProject.ProjectManagerID);
            EmployeeDto rm = employee.ReportingManagerID.HasValue ? employeeRepo.GetByID(employee.ReportingManagerID.Value) : null;
            PracticeDto practice = practiceRepository.GetByID(benchProject.PracticeID);
            EmployeeDto practiceMgr = practice.ManagerID.HasValue ? employeeRepo.GetByID(practice.ManagerID.Value) : null;

            StringBuilder toEmailID = new StringBuilder();
            if (!string.IsNullOrEmpty(pm?.EmailID))
            {
                toEmailID.Append(pm.EmailID);
            }
            else
            {
                toEmailID.Append("satish.srinivasan@agilisium.com");
            }

            StringBuilder bccEmailIDs = new StringBuilder();
            if (!string.IsNullOrEmpty(employee?.EmailID))
            {
                bccEmailIDs.Append(employee.EmailID + ";");
            }

            if (!string.IsNullOrEmpty(rm?.EmailID))
            {
                bccEmailIDs.Append(rm.EmailID + ";");
            }

            if (!string.IsNullOrEmpty(practiceMgr?.EmailID))
            {
                bccEmailIDs.Append(practiceMgr.EmailID + ";");
            }

            bccEmailIDs.Append("satish.srinivasan@agilisium.com");

            string emailSubject = $"RMT Alert - {employee.FirstName} {employee.LastName} had been Moved to Bench Project";
            EmailHandler emailHandler = new EmailHandler(ownerEmailID, outlookPwd);
            string emailContent = GenerateNewAllocationEmailContent(benchProject, employee);

            emailHandler.SendEmail(emailClientIP, toEmailID.ToString(), emailSubject, emailContent, bccEmailIDs.ToString());
        }

        private string GenerateAllocationEndingEmailContent(ProjectAllocationDto allocation)
        {
            string templateFilePath = ProcessorHelper.GetSettingsValue(ProcessorHelper.TEMPLATE_FOLDER_PATH) + "\\AllocationEndingEmailTemplate.html";
            string emailTemplateContent = FilesHandler.GetFileContent(templateFilePath);

            StringBuilder emailBody = new StringBuilder(emailTemplateContent);
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
            emailBody.Replace("__START_DATE__", $"{allocation.AllocationStartDate.Day}/{ci.DateTimeFormat.GetAbbreviatedMonthName(allocation.AllocationStartDate.Month)}/{allocation.AllocationStartDate.Year}");
            emailBody.Replace("__END_DATE__", $"{allocation.AllocationEndDate.Day}/{ci.DateTimeFormat.GetAbbreviatedMonthName(allocation.AllocationEndDate.Month)}/{allocation.AllocationEndDate.Year}");
            emailBody.Replace("__RESOURCE_NAME__", allocation.EmployeeName);
            emailBody.Replace("__PROJECT_NAME__", allocation.ProjectName);
            emailBody.Replace("__RESOURCE_ID__", allocation.EmployeeID.ToString());
            return emailBody.ToString();
        }

        private string GenerateNewAllocationEmailContent(ProjectDto benchProject, EmployeeDto employee)
        {
            string templateFilePath = ProcessorHelper.GetSettingsValue(ProcessorHelper.TEMPLATE_FOLDER_PATH) + "\\NewAllocationEmailTemplate.html";
            string emailTemplateContent = FilesHandler.GetFileContent(templateFilePath);

            StringBuilder emailBody = new StringBuilder(emailTemplateContent);
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
            emailBody.Replace("__START_DATE__", $"{DateTime.Now.Day}/{ci.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month)}/{DateTime.Now.Year}");
            emailBody.Replace("__END_DATE__", $"{benchProject.EndDate.Day}/{ci.DateTimeFormat.GetAbbreviatedMonthName(benchProject.EndDate.Month)}/{benchProject.EndDate.Year}");
            emailBody.Replace("__RESOURCE_NAME__", $"{employee.FirstName} {employee.LastName}");
            emailBody.Replace("__PROJECT_NAME__", benchProject.ProjectName);
            emailBody.Replace("__RESOURCE_ID__", employee.EmployeeID.ToString());
            return emailBody.ToString();
        }

        private string GenerateAllocationFailureEmailContent(ProjectAllocationDto allocation)
        {
            string templateFilePath = ProcessorHelper.GetSettingsValue(ProcessorHelper.TEMPLATE_FOLDER_PATH) + "\\AllocationFailureEmailTemplate.html";
            string emailTemplateContent = FilesHandler.GetFileContent(templateFilePath);

            StringBuilder emailBody = new StringBuilder(emailTemplateContent);
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
            emailBody.Replace("__START_DATE__", $"{allocation.AllocationStartDate.Day}/{ci.DateTimeFormat.GetAbbreviatedMonthName(allocation.AllocationStartDate.Month)}/{allocation.AllocationStartDate.Year}");
            emailBody.Replace("__END_DATE__", $"{allocation.AllocationEndDate.Day}/{ci.DateTimeFormat.GetAbbreviatedMonthName(allocation.AllocationEndDate.Month)}/{allocation.AllocationEndDate.Year}");
            emailBody.Replace("__RESOURCE_NAME__", allocation.EmployeeName);
            emailBody.Replace("__PROJECT_NAME__", allocation.ProjectName);
            emailBody.Replace("__RESOURCE_ID__", allocation.EmployeeID.ToString());
            return emailBody.ToString();
        }
    }
}
