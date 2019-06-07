using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using Agilisium.TalentManager.Web.Helpers;
using Agilisium.TalentManager.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IAllocationService allocationService;
        private readonly IProjectService projectService;
        private readonly IEmployeeService empService;
        private readonly IPracticeService practiceService;
        private readonly IProjectAccountService accountsService;
        private readonly IDropDownSubCategoryService subCategoryService;

        public ReportsController(IAllocationService allocationService, IProjectService projectService,
            IEmployeeService empService, IPracticeService practiceService, IProjectAccountService accountsService,
            IDropDownSubCategoryService subCategoryService)
        {
            this.allocationService = allocationService;
            this.projectService = projectService;
            this.empService = empService;
            this.practiceService = practiceService;
            this.accountsService = accountsService;
            this.subCategoryService = subCategoryService;
        }

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManagerWiseAllocations()
        {
            List<ManagerWiseAllocationModel> managerSummary = new List<ManagerWiseAllocationModel>();
            try
            {
                List<ManagerWiseAllocationDto> summaryDtos = allocationService.GetManagerWiseAllocationSummary();
                managerSummary = Mapper.Map<List<ManagerWiseAllocationDto>, List<ManagerWiseAllocationModel>>(summaryDtos);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(managerSummary);
        }

        public ActionResult ManagerWiseProjects(int id)
        {
            ProjectViewModel model = new ProjectViewModel();
            try
            {
                List<ProjectDto> summaryDtos = projectService.GetAllByManagerID(id);
                model.Projects = Mapper.Map<List<ProjectDto>, List<ProjectModel>>(summaryDtos);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(model);
        }

        public ActionResult UtilizationReportSummary()
        {
            List<BillabilityWiseAllocationSummaryModel> allocations = new List<BillabilityWiseAllocationSummaryModel>();
            try
            {
                List<BillabilityWiseAllocationSummaryDto> summaryDtos = allocationService.GetBillabilityWiseAllocationSummary();
                allocations = Mapper.Map<List<BillabilityWiseAllocationSummaryDto>, List<BillabilityWiseAllocationSummaryModel>>(summaryDtos);
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(allocations);
        }

        public ActionResult UtilizationReportDetail(string filterType, string filterValue)
        {
            UtilizationReportDetailViewModel model = new UtilizationReportDetailViewModel();
            try
            {
                model.FilterType = filterType;
                model.FilterValue = filterValue;
                List<BillabilityWiseAllocationDetailDto> summaryDtos = allocationService.GetBillabilityWiseAllocationDetail(filterType, filterValue);
                model.Allocations = Mapper.Map<List<BillabilityWiseAllocationDetailDto>, List<BillabilityWiseAllocationDetailModel>>(summaryDtos);
                model.FilterValueListItems = GetFilterValueListItems(filterType);
                LoadFilterValueListItems(filterType);
                if(model.Allocations.Count()==0)
                {
                    DisplayWarningMessage("No records found. Please try with different filter conditions");
                }
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadFilterValueListItems(string filterType)
        {
            List<SelectListItem> filterValues = null;
            try
            {
                filterValues = GetFilterValueListItems(filterType);

                filterValues.Insert(0, new SelectListItem
                {
                    Text = "Please Select",
                    Value = "0",
                });
                Session["FilterValueListItems"] = filterValues;
            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            return Json(filterValues);
        }

        public FileStreamResult DownloadAllocationDetails(string filterType, string filterValue)
        {
            StringBuilder recordString = new StringBuilder($"Employee ID,Employee Name,Primary Skills,Secondary Skills,Business Unit,POD,Project Name,Account Name,Allocation Type,Allocation Start Date,Allocation End Date,Project Manager,Comments{Environment.NewLine}");
            try
            {
                List<BillabilityWiseAllocationDetailDto> detailsDtos = allocationService.GetBillabilityWiseAllocationDetail(filterType, filterValue);
                foreach (BillabilityWiseAllocationDetailDto dto in detailsDtos)
                {
                    recordString.Append($"{dto.EmployeeID},");
                    recordString.Append($"{dto.EmployeeName},");
                    recordString.Append($"{dto.PrimarySkills?.Replace(",", "")},");
                    recordString.Append($"{dto.SecondarySkills?.Replace(",", "")},");
                    recordString.Append($"{dto.BusinessUnit},");
                    recordString.Append($"{dto.POD},");
                    recordString.Append($"{dto.ProjectName},");
                    recordString.Append($"{dto.AccountName},");
                    recordString.Append($"{dto.AllocationType},");
                    recordString.Append($"{dto.AllocationStartDate?.ToString("dd/MMM/yyyy")},");
                    recordString.Append($"{dto.AllocationEndDate?.ToString("dd/MMM/yyyy")},");
                    recordString.Append($"{dto.ProjectManager},");
                    recordString.Append($"{dto.Comments}{Environment.NewLine}");
                }

            }
            catch (Exception exp)
            {
                DisplayLoadErrorMessage(exp);
            }
            byte[] byteArr = Encoding.ASCII.GetBytes(recordString.ToString());
            MemoryStream stream = new MemoryStream(byteArr);
            return File(stream, "application/vnd.ms-excel", "Resource Allocation Report.csv");
        }

        #region Private Methods

        private List<SelectListItem> GetEmployeesList()
        {
            List<EmployeeDto> employees = empService.GetAllEmployees("");

            List<SelectListItem> pmList = (from e in employees
                                           select new SelectListItem
                                           {
                                               Text = $"{e.FirstName} {e.LastName}",
                                               Value = e.EmployeeEntryID.ToString()
                                           }).OrderBy(i => i.Text).ToList();

            return pmList;
        }

        private List<SelectListItem> GetProjectsList()
        {
            IEnumerable<ProjectDto> projects = projectService.GetAll();

            List<SelectListItem> projectList = (from p in projects
                                                orderby p.ProjectCode
                                                select new SelectListItem
                                                {
                                                    Text = $"{p.ProjectCode}-{p.ProjectName}",
                                                    Value = p.ProjectID.ToString()
                                                }).OrderBy(i => i.Text).ToList();

            return projectList;
        }

        public List<SelectListItem> GetPracticeList()
        {
            List<PracticeDto> practices = practiceService.GetPractices().ToList();
            List<SelectListItem> practiceItems = (from p in practices
                                                  orderby p.PracticeName
                                                  select new SelectListItem
                                                  {
                                                      Text = p.PracticeName,
                                                      Value = p.PracticeID.ToString()
                                                  }).ToList();
            return practiceItems;
        }

        private List<SelectListItem> GetAllAccountsList()
        {
            List<ProjectAccountDto> accounts = accountsService.GetAll();

            List<SelectListItem> accDDList = (from e in accounts
                                              select new SelectListItem
                                              {
                                                  Text = e.AccountName,
                                                  Value = e.AccountID.ToString()
                                              }).ToList();

            return accDDList;
        }

        private List<SelectListItem> GetOtherDropDownItems()
        {
            IEnumerable<DropDownSubCategoryDto> buList = subCategoryService.GetSubCategories((int)CategoryType.UtilizationCode);

            List<SelectListItem> projectTypeItems = (from c in buList
                                                     orderby c.SubCategoryName
                                                     where c.CategoryID == (int)CategoryType.UtilizationCode
                                                     select new SelectListItem
                                                     {
                                                         Text = c.SubCategoryName,
                                                         Value = c.SubCategoryID.ToString()
                                                     }).ToList();
            projectTypeItems.Add(new SelectListItem
            {
                Text = "Not Allocated Yet (Delivery)",
                Value = "-1",
            });

            projectTypeItems.Add(new SelectListItem
            {
                Text = "Not Allocated Yet (Non-Delivery)",
                Value = "-2",
            });

            return projectTypeItems;
        }

        private List<SelectListItem> GetFilterValueListItems(string filterType)
        {
            List<SelectListItem> filterValues = new List<SelectListItem>();
            switch (filterType?.ToLower())
            {
                case "emp":
                    filterValues = GetEmployeesList();
                    break;
                case "pod":
                    filterValues = GetPracticeList();
                    break;
                case "prj":
                    filterValues = GetProjectsList();
                    break;
                case "acc":
                    filterValues = GetAllAccountsList();
                    break;
                case "alt":
                    filterValues = GetOtherDropDownItems();
                    break;
            }

            return filterValues;
        }

        #endregion
    }
}