using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        #region Constants

        private const string YET_TO_JOIN_EMP_TYPE_NAME = "Yet to Join";
        private const string PERMANENT_EMP_TYPE_NAME = "Permanent";

        #endregion

        #region Public Methods - Employee Repository

        public bool Exists(string itemName)
        {
            return Entities.Any(e => e.FirstName.ToLower() == itemName.ToLower() && e.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(e => e.EmployeeEntryID == id && e.IsDeleted == false);
        }

        public EmployeeDto GetByID(int id)
        {
            return (from emp in Entities
                    join bc in DataContext.DropDownSubCategories on emp.BusinessUnitID equals bc.SubCategoryID into bue
                    from bcd in bue.DefaultIfEmpty()
                    join pc in DataContext.Practices on emp.PracticeID equals pc.PracticeID into pce
                    from pcd in pce.DefaultIfEmpty()
                    join ut in DataContext.DropDownSubCategories on emp.UtilizationTypeID equals ut.SubCategoryID into ute
                    from utd in ute.DefaultIfEmpty()
                    join et in DataContext.DropDownSubCategories on emp.EmploymentTypeID equals et.SubCategoryID into ete
                    from etd in ete.DefaultIfEmpty()
                    join pm in DataContext.Employees on emp.EmployeeEntryID equals pm.EmployeeEntryID into pme
                    from pmd in pme.DefaultIfEmpty()
                    where emp.EmployeeEntryID == id
                    select new EmployeeDto
                    {
                        BusinessUnitID = emp.BusinessUnitID,
                        DateOfJoin = emp.DateOfJoin,
                        EmployeeEntryID = emp.EmployeeEntryID,
                        EmailID = emp.EmailID,
                        EmployeeID = emp.EmployeeID,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        LastWorkingDay = emp.LastWorkingDay,
                        PracticeID = emp.PracticeID,
                        PrimarySkills = emp.PrimarySkills,
                        ProjectManagerID = pmd.EmployeeEntryID,
                        ProjectManagerName = pmd.LastName + ", " + pmd.FirstName,
                        ReportingManagerID = emp.ReportingManagerID,
                        SecondarySkills = emp.SecondarySkills,
                        SubPracticeID = emp.SubPracticeID,
                        UtilizationTypeID = emp.UtilizationTypeID,
                        EmploymentTypeID = emp.EmploymentTypeID,
                        EmploymentTypeName = etd.SubCategoryName
                    }).FirstOrDefault();
        }

        public IEnumerable<EmployeeDto> GetAll(string searchText, int pageSize = -1, int pageNo = -1)
        {
            IEnumerable<EmployeeDto> employees = null;
            try
            {
                employees = GetAllActiveEmployees(searchText);

                if (pageSize <= 0 || pageNo < 1)
                {
                    return employees;
                }

            }
            catch (Exception)
            {

            }
            return employees.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<EmployeeDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IEnumerable<EmployeeDto> employees = GetAllActiveEmployees("");

            if (pageSize <= 0 || pageNo < 1)
            {
                return employees;
            }

            return employees.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<EmployeeDto> GetAllPastEmployees(int pageSize = -1, int pageNo = -1)
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();

            List<Employee> pastEmployees = (from emp in Entities
                                            where emp.LastWorkingDay.HasValue == true
                                            || emp.IsDeleted == true
                                            select emp).ToList();

            foreach (Employee emp in pastEmployees)
            {
                employees.Add(new EmployeeDto
                {
                    BusinessUnitName = DataContext.DropDownSubCategories.FirstOrDefault(b => b.SubCategoryID == emp.BusinessUnitID)?.SubCategoryName,
                    DateOfJoin = emp.DateOfJoin,
                    EmployeeEntryID = emp.EmployeeEntryID,
                    EmployeeID = emp.EmployeeID,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    PracticeName = DataContext.Practices.FirstOrDefault(b => b.PracticeID == emp.PracticeID)?.PracticeName,
                    PrimarySkills = emp.PrimarySkills,
                    UtilizationTypeName = DataContext.DropDownSubCategories.FirstOrDefault(b => b.SubCategoryID == emp.UtilizationTypeID)?.SubCategoryName,
                    EmploymentTypeName = DataContext.DropDownSubCategories.FirstOrDefault(b => b.SubCategoryID == emp.EmploymentTypeID)?.SubCategoryName,
                    SubPracticeName = DataContext.SubPractices.FirstOrDefault(b => b.SubPracticeID == emp.SubPracticeID)?.SubPracticeName,
                    LastWorkingDay = emp.LastWorkingDay,
                    ReportingManagerName = Entities.FirstOrDefault(e => e.ReportingManagerID == emp.ReportingManagerID)?.FirstName
                });
            }

            if (pageSize <= 0 || pageNo < 1)
            {
                return employees;
            }

            return employees.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public int GetPastEmployeesCount()
        {
            return Entities.Count(emp => emp.LastWorkingDay.HasValue == true || emp.IsDeleted == true);
        }

        public bool IsDuplicateName(string firstName, string lastName)
        {
            return Entities.Any(e =>
                e.FirstName.ToLower() == firstName.ToLower() &&
                e.LastName.ToLower() == lastName.ToLower() && e.IsDeleted == false);
        }

        public bool IsDuplicateName(int employeeEntryID, string firstName, string lastName)
        {
            return Entities.Any(e =>
                e.EmployeeEntryID != employeeEntryID &&
                e.FirstName.ToLower() == firstName.ToLower() &&
                e.LastName.ToLower() == lastName.ToLower() && e.IsDeleted == false);
        }

        public bool IsDuplicateEmployeeID(string employeeID)
        {
            return Entities.Any(e => e.EmployeeID.ToLower() == employeeID.ToLower() && e.IsDeleted == false);
        }

        public void Add(EmployeeDto entity)
        {
            Employee employee = CreateBusinessEntity(entity, true);
            Entities.Add(employee);
            DataContext.Entry(employee).State = EntityState.Added;
            DataContext.SaveChanges();

            UpdateEmployeeIDTracker(entity.EmploymentTypeID, entity.EmployeeID);
        }

        public void Update(EmployeeDto entity)
        {
            Employee buzEntity = Entities.FirstOrDefault(e => e.EmployeeEntryID == entity.EmployeeEntryID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();

            UpdateEmployeeIDTracker(entity.EmploymentTypeID, entity.EmployeeID);
        }

        public void Delete(EmployeeDto entity)
        {
            Employee buzEntity = Entities.FirstOrDefault(e => e.EmployeeEntryID == entity.EmployeeEntryID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool IsDuplicateEmployeeID(int employeeEntryID, string employeeID)
        {
            return Entities.Any(e => e.EmployeeID.ToLower() == employeeID.ToLower() &&
            e.EmployeeEntryID != employeeEntryID && e.IsDeleted == false);
        }

        public IEnumerable<EmployeeDto> GetAllManagers()
        {
            return (from emp in Entities
                    join sp in DataContext.SubPractices on emp.SubPracticeID equals sp.SubPracticeID
                    where emp.IsDeleted == false && sp.SubPracticeName == "Project Management"
                    orderby emp.EmployeeID
                    select new EmployeeDto
                    {
                        EmployeeEntryID = emp.EmployeeEntryID,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName
                    });
        }

        public IEnumerable<EmployeeDto> GetAllAccountManagers()
        {
            return GetAllManagers();
        }

        public string GenerateNewEmployeeID(int employeeTypeID)
        {
            string employeeID = string.Empty;

            EmployeeIDTracker tracker = DataContext.EmployeeIDTrackers.FirstOrDefault(e => e.EmploymentTypeID == employeeTypeID);
            string empType = DataContext.DropDownSubCategories.FirstOrDefault(s => s.SubCategoryID == employeeTypeID)?.SubCategoryName;

            bool isDuplicate = true;

            int runningID = tracker.RunningID;
            int newRunningID = tracker.RunningID + 1;
            string idPrefix = tracker.IDPrefix;
            while (isDuplicate)
            {
                if (empType == PERMANENT_EMP_TYPE_NAME)
                {
                    employeeID = newRunningID.ToString();
                }
                else
                {
                    employeeID = $"{tracker.IDPrefix}{newRunningID.ToString().PadLeft(3, '0')}";
                }

                if (!Entities.Any(e => e.EmployeeID.ToLower() == employeeID.ToLower()))
                {
                    isDuplicate = false;
                }
                else
                {
                    newRunningID += 1;
                }
            }

            UpdateEmployeeIDTracker(employeeTypeID, runningID);

            return employeeID.ToUpper();
        }

        public int TotalRecordsCount(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return TotalRecordsCount();
            }
            else
            {
                return Entities.Count(e => e.IsDeleted == false
                && (e.FirstName.ToLower().StartsWith(searchText.ToLower())
                || e.LastName.ToLower().StartsWith(searchText.ToLower())));
            }
        }

        public IEnumerable<PracticeHeadCountDto> GetPracticeWiseHeadCount()
        {
            return (from e in Entities
                    where e.IsDeleted == false && e.LastWorkingDay.HasValue == false
                    group e by e.PracticeID into eg
                    orderby eg.Count() descending
                    select new PracticeHeadCountDto
                    {
                        HeadCount = eg.Count(emp => emp.IsDeleted == false),
                        PracticeID = eg.Key,
                        Practice = DataContext.Practices.FirstOrDefault(p => p.PracticeID == eg.Key).PracticeName
                    });
        }

        public IEnumerable<SubPracticeHeadCountDto> GetSubPracticeWiseHeadCount()
        {
            DbCommand cmd = DataContext.Database.Connection.CreateCommand();
            cmd.CommandText = "dbo.GetSubPracticeWiseHeadCount";
            cmd.CommandType = CommandType.StoredProcedure;
            DataContext.Database.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader();
            ObjectResult<SubPracticeHeadCountDto> items = ((IObjectContextAdapter)DataContext).ObjectContext.Translate<SubPracticeHeadCountDto>(reader);

            List<SubPracticeHeadCountDto> records = items.ToList();
            DataContext.Database.Connection.Close();
            return records;
        }

        public IEnumerable<EmployeeDto> GetAllByPractice(int practiceID, int pageSize = -1, int pageNo = -1)
        {
            IEnumerable<EmployeeDto> employees = GetAllActiveEmployees("").Where(e => e.PracticeID == practiceID);

            if (pageSize <= 0 || pageNo < 1)
            {
                return employees;
            }

            return employees.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<EmployeeDto> GetAllBySubPractice(int subPracticeID, int pageSize = -1, int pageNo = -1)
        {
            IEnumerable<EmployeeDto> employees = GetAllActiveEmployees("")
                                                .Where(e => e.SubPracticeID == subPracticeID);

            if (pageSize <= 0 || pageNo < 1)
            {
                return employees;
            }

            return employees.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public int PracticeWiseRecordsCount(int practiceID)
        {
            return Entities.Count(e => e.IsDeleted == false && e.PracticeID == practiceID);
        }

        public int SubPracticeWiseRecordsCount(int subPracticeID)
        {
            return Entities.Count(e => e.IsDeleted == false && e.SubPracticeID == subPracticeID);
        }

        public ResourceCountDto GetEmployeesCountSummary()
        {
            ResourceCountDto dto = new ResourceCountDto
            {
                TotalCount = Entities.Count(e => e.IsDeleted == false && e.LastWorkingDay.HasValue == false),
                DeliveryCount = GetEmployeesCountByBU(BusinessUnit.Delivery),
                BoCount = GetEmployeesCountByBU(BusinessUnit.BusinessOperations),
                BdCount = GetEmployeesCountByBU(BusinessUnit.BusinessDevelopment),
            };

            return dto;
        }

        public BillabilityWiseResourceCountDto GetBillabilityCountSummary()
        {
            BillabilityWiseResourceCountDto dto = new BillabilityWiseResourceCountDto
            {
                BillableCount = GetEmployeesCountByAllocationType(AllocationType.Billable),
                CommittedBufferCount = GetEmployeesCountByAllocationType(AllocationType.CommittedBuffer),
                NonCommittedBufferCount = GetEmployeesCountByAllocationType(AllocationType.NonCommittedBuffer),
            };

            return dto;
        }

        public string GetNameByEmployeeID(string empID)
        {
            return Entities.FirstOrDefault(e => e.EmployeeID == empID)?.FirstName;
        }

        #endregion

        #region Private Methods

        private void UpdateEmployeeIDTracker(int trackerID, int runningID)
        {
            EmployeeIDTracker tracker = DataContext.EmployeeIDTrackers.FirstOrDefault(e => e.EmploymentTypeID == trackerID);
            tracker.RunningID = runningID;
            DataContext.EmployeeIDTrackers.Add(tracker);
            DataContext.Entry(tracker).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private bool UpdateEmployeeIDTracker(int employeeType, string employeeID)
        {
            EmployeeIDTracker tracker = DataContext.EmployeeIDTrackers.FirstOrDefault(e => e.EmploymentTypeID == employeeType);

            if (string.IsNullOrEmpty(tracker.IDPrefix) == false)
            {
                employeeID = employeeID.Replace(tracker.IDPrefix, "");
            }

            if (int.TryParse(employeeID, out int runningID))
            {
                tracker.RunningID = runningID;
                DataContext.EmployeeIDTrackers.Add(tracker);
                DataContext.Entry(tracker).State = EntityState.Modified;
                DataContext.SaveChanges();
                return true;
            }

            return false;
        }

        private IEnumerable<EmployeeDto> GetAllActiveEmployees(string searchText)
        {
            IQueryable<EmployeeDto> employees = null;
            employees = from emp in Entities
                        join bc in DataContext.DropDownSubCategories on emp.BusinessUnitID equals bc.SubCategoryID into bue
                        from bcd in bue.DefaultIfEmpty()
                        join pc in DataContext.Practices on emp.PracticeID equals pc.PracticeID into pce
                        from pcd in pce.DefaultIfEmpty()
                        join ut in DataContext.DropDownSubCategories on emp.UtilizationTypeID equals ut.SubCategoryID into ute
                        from utd in ute.DefaultIfEmpty()
                        join et in DataContext.DropDownSubCategories on emp.EmploymentTypeID equals et.SubCategoryID into ete
                        from etd in ete.DefaultIfEmpty()
                        join sp in DataContext.SubPractices on emp.SubPracticeID equals sp.SubPracticeID into spe
                        from spd in spe.DefaultIfEmpty()
                        join rm in Entities on emp.ReportingManagerID equals rm.EmployeeEntryID into rme
                        from rmd in rme.DefaultIfEmpty()

                        where emp.IsDeleted == false && emp.LastWorkingDay.HasValue == false
                        orderby emp.EmployeeID
                        select new EmployeeDto
                        {
                            BusinessUnitName = bcd.SubCategoryName,
                            DateOfJoin = emp.DateOfJoin,
                            EmployeeEntryID = emp.EmployeeEntryID,
                            EmployeeID = emp.EmployeeID,
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            PracticeID = emp.PracticeID,
                            PracticeName = pcd.PracticeName,
                            PrimarySkills = emp.PrimarySkills,
                            UtilizationTypeName = utd.SubCategoryName,
                            EmploymentTypeName = etd.SubCategoryName,
                            SubPracticeID = emp.SubPracticeID,
                            SubPracticeName = spd.SubPracticeName,
                            ReportingManagerName = string.IsNullOrEmpty(rmd.FirstName) ? "" : rmd.LastName + ", " + rmd.FirstName,
                        };

            if (string.IsNullOrEmpty(searchText) == false)
            {
                employees = employees.Where(e => e.FirstName.ToLower().StartsWith(searchText) || e.LastName.ToLower().StartsWith(searchText));
            }
            return employees;
        }

        private Employee CreateBusinessEntity(EmployeeDto employeeDto, bool isNewEntity = false)
        {
            Employee employee = new Employee
            {
                BusinessUnitID = employeeDto.BusinessUnitID,
                DateOfJoin = new DateTime(employeeDto.DateOfJoin.Year, employeeDto.DateOfJoin.Month, employeeDto.DateOfJoin.Day),
                EmailID = employeeDto.EmailID,
                EmployeeID = employeeDto.EmployeeID,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                LastWorkingDay = employeeDto.LastWorkingDay,
                PracticeID = employeeDto.PracticeID,
                PrimarySkills = employeeDto.PrimarySkills,
                ReportingManagerID = employeeDto.ReportingManagerID,
                SecondarySkills = employeeDto.SecondarySkills,
                SubPracticeID = employeeDto.SubPracticeID,
                UtilizationTypeID = employeeDto.UtilizationTypeID,
                EmployeeEntryID = employeeDto.EmployeeEntryID,
                EmploymentTypeID = employeeDto.EmploymentTypeID,
            };

            employee.UpdateTimeStamp(employeeDto.LoggedInUserName, true);
            return employee;
        }

        private void MigrateEntity(EmployeeDto sourceEntity, Employee targetEntity)
        {
            targetEntity.BusinessUnitID = sourceEntity.BusinessUnitID;
            targetEntity.DateOfJoin = new DateTime(sourceEntity.DateOfJoin.Year, sourceEntity.DateOfJoin.Month, sourceEntity.DateOfJoin.Day);
            targetEntity.EmailID = sourceEntity.EmailID;
            targetEntity.EmployeeID = sourceEntity.EmployeeID;
            targetEntity.FirstName = sourceEntity.FirstName;
            targetEntity.LastName = sourceEntity.LastName;
            targetEntity.LastWorkingDay = sourceEntity.LastWorkingDay;
            targetEntity.PracticeID = sourceEntity.PracticeID;
            targetEntity.PrimarySkills = sourceEntity.PrimarySkills;
            targetEntity.ReportingManagerID = sourceEntity.ReportingManagerID;
            targetEntity.SecondarySkills = sourceEntity.SecondarySkills;
            targetEntity.SubPracticeID = sourceEntity.SubPracticeID;
            targetEntity.UtilizationTypeID = sourceEntity.UtilizationTypeID;
            targetEntity.EmployeeEntryID = sourceEntity.EmployeeEntryID;
            targetEntity.EmploymentTypeID = sourceEntity.EmploymentTypeID;

            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }

        private int GetEmployeesCountByAllocationType(AllocationType allocationType)
        {
            return (from e in Entities
                    join a in DataContext.ProjectAllocations on e.EmployeeEntryID equals a.EmployeeID
                    where a.AllocationTypeID == (int)allocationType
                    && e.LastWorkingDay.HasValue == false && e.IsDeleted == false && a.IsDeleted == false
                    && a.AllocationEndDate >= DateTime.Now
                    select a.EmployeeID).Distinct().Count();
        }

        private int GetEmployeesCountByBU(BusinessUnit bu)
        {
            return Entities.Count(e => e.LastWorkingDay.HasValue == false && e.IsDeleted == false && e.BusinessUnitID == (int)bu);
        }

        #endregion
    }

    public interface IEmployeeRepository : IRepository<EmployeeDto>
    {
        bool IsDuplicateName(string firstName, string lastName);

        bool IsDuplicateName(int employeeEntryID, string firstName, string lastName);

        bool IsDuplicateEmployeeID(string employeeID);

        bool IsDuplicateEmployeeID(int employeeEntryID, string employeeID);

        string GenerateNewEmployeeID(int employeeTypeID);

        IEnumerable<EmployeeDto> GetAllManagers();

        IEnumerable<EmployeeDto> GetAllPastEmployees(int pageSize = -1, int pageNo = -1);

        int GetPastEmployeesCount();

        int TotalRecordsCount(string searchText);

        IEnumerable<EmployeeDto> GetAll(string searchText, int pageSize = -1, int pageNo = -1);

        IEnumerable<PracticeHeadCountDto> GetPracticeWiseHeadCount();

        IEnumerable<SubPracticeHeadCountDto> GetSubPracticeWiseHeadCount();

        IEnumerable<EmployeeDto> GetAllByPractice(int practiceID, int pageSize = -1, int pageNo = -1);

        IEnumerable<EmployeeDto> GetAllBySubPractice(int subPracticeID, int pageSize = -1, int pageNo = -1);

        int PracticeWiseRecordsCount(int practiceID);

        int SubPracticeWiseRecordsCount(int subPracticeID);

        ResourceCountDto GetEmployeesCountSummary();

        IEnumerable<EmployeeDto> GetAllAccountManagers();

        string GetNameByEmployeeID(string empID);

        BillabilityWiseResourceCountDto GetBillabilityCountSummary();
    }
}
