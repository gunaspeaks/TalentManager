using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.PostgresDbHelper;
using Agilisium.TalentManager.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class AllocationRepository : RepositoryBase<ProjectAllocation>, IAllocationRepository
    {
        private readonly PostgresSqlProcessor postgresSqlProcessor = null;

        public AllocationRepository()
        {
            postgresSqlProcessor = new PostgresSqlProcessor();
        }

        #region Public Methods

        public void Add(ProjectAllocationDto entity)
        {
            ProjectAllocation allocation = CreateBusinessEntity(entity, true);
            Entities.Add(allocation);
            DataContext.Entry(allocation).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(ProjectAllocationDto entity)
        {
            ProjectAllocation allocation = Entities.FirstOrDefault(e => e.AllocationEntryID == entity.AllocationEntryID);
            allocation.IsDeleted = true;
            allocation.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(allocation);
            DataContext.Entry(allocation).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public int Exists(int empEntryID, int projectID)
        {
            return Entities.Count(a => a.EmployeeID == empEntryID &&
            a.ProjectID == projectID && a.IsDeleted == false);
        }

        public int Exists(int allocationID, int empEntryID, int projectID)
        {
            return Entities.Count(a => a.AllocationEntryID != allocationID &&
            a.EmployeeID == empEntryID && a.ProjectID == projectID && a.IsDeleted == false);
        }

        public bool Exists(int id)
        {
            return Entities.Any(a => a.AllocationEntryID == id && a.IsDeleted == false);
        }

        public bool Exists(string projectName)
        {
            return (from a in Entities
                    join p in DataContext.Projects on a.ProjectID equals p.ProjectID into pe
                    from ped in pe.DefaultIfEmpty()
                    where ped.ProjectName == projectName && a.IsDeleted == false && ped.IsDeleted == false
                    select a).Any();
        }

        public IEnumerable<ProjectAllocationDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            return GetAll("", 0, "", "", pageSize, pageNo);
        }

        public IQueryable<ProjectAllocationDto> GetAllRecords(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<ProjectAllocationDto> allocations = from p in Entities
                                                           join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                                                           from emd in eme.DefaultIfEmpty()
                                                           join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                                                           from scd in sce.DefaultIfEmpty()
                                                           join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                                                           from prd in pre.DefaultIfEmpty()
                                                           join pm in DataContext.Employees on prd.ProjectManagerID equals pm.EmployeeEntryID into pme
                                                           from pmd in pme.DefaultIfEmpty()
                                                           join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                                                           from acd in ace.DefaultIfEmpty()
                                                           where p.IsDeleted == false && p.AllocationEndDate >= DateTime.Now
                                                           orderby prd.ProjectName, p.AllocationStartDate
                                                           select new ProjectAllocationDto
                                                           {
                                                               AllocationEndDate = p.AllocationEndDate,
                                                               AllocationEntryID = p.AllocationEntryID,
                                                               AllocationStartDate = p.AllocationStartDate,
                                                               AllocationTypeName = scd.SubCategoryName,
                                                               EmployeeName = emd.FirstName + " " + emd.LastName,
                                                               ProjectManagerName = pmd.FirstName + " " + pmd.LastName,
                                                               EmployeeID = p.EmployeeID,
                                                               ProjectName = prd.ProjectName,
                                                               Remarks = p.Remarks,
                                                               PercentageOfAllocation = p.PercentageOfAllocation,
                                                               AccountName = acd.AccountName,
                                                               ProjectID = prd.ProjectID
                                                           };
            return allocations;

        }

        public IEnumerable<ProjectAllocationDto> GetAll(string filterType, int filterValueID, string sortBy = "empname", string sortType = "asc", int pageSize = -1, int pageNo = -1)
        {
            IQueryable<ProjectAllocationDto> allocations = null;
            if (string.IsNullOrWhiteSpace(filterType))
            {
                allocations = GetAllRecords(pageSize, pageNo);
            }

            switch (filterType?.ToLower())
            {
                case "prj":
                    allocations = GetAllocationsByProjectID(filterValueID);
                    break;
                case "pm":
                    allocations = GetAllocationsByProjectManagerID(filterValueID);
                    break;
                case "emp":
                    allocations = GetAllocationsByEmployeeID(filterValueID);
                    break;
            }

            allocations = SortAllocationItems(allocations, sortBy, sortType);

            if (pageSize < 0)
            {
                return allocations;
            }
            return allocations.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<ProjectAllocationDto> GetAllAllocationsByProjectID(int projectID)
        {
            return from p in Entities
                   join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                   from emd in eme.DefaultIfEmpty()
                   join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                   from scd in sce.DefaultIfEmpty()
                   join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                   from prd in pre.DefaultIfEmpty()
                   join pm in DataContext.Employees on prd.ProjectManagerID equals pm.EmployeeEntryID into pme
                   from pmd in pme.DefaultIfEmpty()
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false && p.ProjectID == projectID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.FirstName + " " + emd.LastName,
                       ProjectManagerName = pmd.FirstName + " " + pmd.LastName,
                       EmployeeID = p.EmployeeID,
                       ProjectName = prd.ProjectName,
                       Remarks = p.Remarks,
                       PercentageOfAllocation = p.PercentageOfAllocation,
                       AccountName = acd.AccountName
                   };

        }

        private IQueryable<ProjectAllocationDto> GetAllocationsByEmployeeID(int empID)
        {
            return from p in Entities
                   join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                   from emd in eme.DefaultIfEmpty()
                   join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                   from scd in sce.DefaultIfEmpty()
                   join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                   from prd in pre.DefaultIfEmpty()
                   join pm in DataContext.Employees on prd.ProjectManagerID equals pm.EmployeeEntryID into pme
                   from pmd in pme.DefaultIfEmpty()
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false && p.AllocationEndDate >= DateTime.Now && p.EmployeeID == empID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.FirstName + " " + emd.LastName,
                       ProjectManagerName = pmd.FirstName + " " + pmd.LastName,
                       EmployeeID = p.EmployeeID,
                       ProjectName = prd.ProjectName,
                       Remarks = p.Remarks,
                       PercentageOfAllocation = p.PercentageOfAllocation,
                       AccountName = acd.AccountName
                   };
        }

        private IQueryable<ProjectAllocationDto> GetAllocationsByProjectID(int projectID)
        {
            return from p in Entities
                   join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                   from emd in eme.DefaultIfEmpty()
                   join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                   from scd in sce.DefaultIfEmpty()
                   join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                   from prd in pre.DefaultIfEmpty()
                   join pm in DataContext.Employees on prd.ProjectManagerID equals pm.EmployeeEntryID into pme
                   from pmd in pme.DefaultIfEmpty()
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false && p.AllocationEndDate >= DateTime.Now && p.ProjectID == projectID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.FirstName + " " + emd.LastName,
                       ProjectManagerName = pmd.FirstName + " " + pmd.LastName,
                       EmployeeID = p.EmployeeID,
                       ProjectName = prd.ProjectName,
                       Remarks = p.Remarks,
                       PercentageOfAllocation = p.PercentageOfAllocation,
                       AccountName = acd.AccountName
                   };
        }

        private IQueryable<ProjectAllocationDto> GetAllocationsByProjectManagerID(int managerID)
        {
            return from p in Entities
                   join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                   from emd in eme.DefaultIfEmpty()
                   join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                   from scd in sce.DefaultIfEmpty()
                   join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                   from prd in pre.DefaultIfEmpty()
                   join pm in DataContext.Employees on prd.ProjectManagerID equals pm.EmployeeEntryID into pme
                   from pmd in pme.DefaultIfEmpty()
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false && p.AllocationEndDate >= DateTime.Now && prd.ProjectManagerID == managerID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.FirstName + " " + emd.LastName,
                       ProjectManagerName = pmd.FirstName + " " + pmd.LastName,
                       EmployeeID = p.EmployeeID,
                       ProjectName = prd.ProjectName,
                       Remarks = p.Remarks,
                       PercentageOfAllocation = p.PercentageOfAllocation,
                       AccountName = acd.AccountName
                   };
        }

        public ProjectAllocationDto GetByID(int id)
        {
            return (from p in Entities
                    where p.AllocationEntryID == id
                    select new ProjectAllocationDto
                    {
                        AllocationEndDate = p.AllocationEndDate,
                        AllocationEntryID = p.AllocationEntryID,
                        AllocationStartDate = p.AllocationStartDate,
                        AllocationTypeID = p.AllocationTypeID,
                        EmployeeID = p.EmployeeID,
                        ProjectID = p.ProjectID,
                        Remarks = p.Remarks,
                        PercentageOfAllocation = p.PercentageOfAllocation,
                        IsActive = p.IsActive
                    }).FirstOrDefault();
        }

        public void Update(ProjectAllocationDto entity)
        {
            ProjectAllocation buzEntity = Entities.FirstOrDefault(e => e.AllocationEntryID == entity.AllocationEntryID);
            MigrateEntity(entity, buzEntity);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public int GetPercentageOfAllocation(int employeeID)
        {
            if (Entities.Any(a => a.EmployeeID == employeeID && a.AllocationEndDate >= DateTime.Now))
            {
                return Entities.Where(a => a.EmployeeID == employeeID && a.AllocationEndDate >= DateTime.Now)
                    .Sum(p => p.PercentageOfAllocation);
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<CustomAllocationDto> GetAllocatedProjectsByEmployeeID(int employeeID)
        {
            return (from a in Entities
                    join p in DataContext.Projects on a.ProjectID equals p.ProjectID into pe
                    from pd in pe.DefaultIfEmpty()
                    join sc in DataContext.DropDownSubCategories on a.AllocationTypeID equals sc.SubCategoryID into sce
                    from scd in sce.DefaultIfEmpty()
                    join e in DataContext.Employees on a.EmployeeID equals e.EmployeeEntryID into ee
                    from ed in ee.DefaultIfEmpty()
                    join bu in DataContext.DropDownSubCategories on ed.BusinessUnitID equals bu.SubCategoryID into bue
                    from bud in bue.DefaultIfEmpty()
                    join pr in DataContext.Practices on ed.PracticeID equals pr.PracticeID into pre
                    from prd in pre.DefaultIfEmpty()
                    join sp in DataContext.SubPractices on ed.SubPracticeID equals sp.SubPracticeID into spe
                    from spd in spe.DefaultIfEmpty()
                    join pm in DataContext.Employees on pd.ProjectManagerID equals pm.EmployeeEntryID into pme
                    from pmd in pme.DefaultIfEmpty()
                    join dm in DataContext.Employees on pd.ProjectManagerID equals dm.EmployeeEntryID into dme
                    from dmd in dme.DefaultIfEmpty()
                    where a.EmployeeID == employeeID && a.AllocationEndDate >= DateTime.Now
                    select new CustomAllocationDto
                    {
                        AllocatedPercentage = a.PercentageOfAllocation,
                        EndDate = pd.EndDate,
                        ProjectCode = pd.ProjectCode,
                        ProjectManager = string.IsNullOrEmpty(pmd.FirstName) ? "" : pmd.LastName + ", " + pmd.FirstName,
                        ProjectName = pd.ProjectName,
                        StartDate = pd.StartDate,
                        UtilizatinType = scd.SubCategoryName,
                        BusinessUnit = bud.SubCategoryName,
                        Practice = prd.PracticeName,
                        SubPractice = spd.SubPracticeName,
                        DeliveryManager = string.IsNullOrEmpty(dmd.FirstName) ? "" : dmd.LastName + ", " + dmd.FirstName,
                    });
        }

        public int GetTotalCountForAllocationHistory(string filterType, int filterValue)
        {
            int recordsCount = 0;
            if (string.IsNullOrWhiteSpace(filterType) || filterType?.ToLower() == "please select")
            {
                recordsCount = (from a in Entities
                                where a.IsDeleted == false && a.AllocationEndDate <= DateTime.Now
                                select a).Count();
            }

            switch (filterType?.ToLower())
            {
                case "emp":
                    recordsCount = (from a in Entities
                                    where a.IsDeleted == false && a.AllocationEndDate < DateTime.Now
                                        && a.EmployeeID == filterValue
                                    select a).Count();
                    break;
                case "prj":
                    recordsCount = (from a in Entities
                                    where a.IsDeleted == false & a.AllocationEndDate < DateTime.Now
                                        && a.ProjectID == filterValue
                                    select a).Count();
                    break;
                case "pm":
                    recordsCount = (from a in Entities
                                    join p in DataContext.Projects on a.ProjectID equals p.ProjectID into pe
                                    from pd in pe.DefaultIfEmpty()
                                    where a.IsDeleted == false && a.AllocationEndDate < DateTime.Now
                                        && pd.ProjectManagerID == filterValue
                                    select a).Count();
                    break;
            }

            return recordsCount;
        }

        public IEnumerable<ProjectAllocationDto> GetAllocationHistory(string filterType, int filterValue, string sortBy, string sortType, int pageSize = -1, int pageNo = -1)
        {
            IQueryable<ProjectAllocationDto> allocations = null;

            if (string.IsNullOrWhiteSpace(filterType) || filterType?.ToLower() == "please select")
            {
                allocations = GetAllAllocationHistory();
            }

            switch (filterType)
            {
                case "emp":
                    allocations = GetAllAllocationHistoryByEmployeeID(filterValue);
                    break;
                case "prj":
                    allocations = GetAllAllocationHistoryByProject(filterValue);
                    break;
                case "pm":
                    allocations = GetAllAllocationHistoryByManagerID(filterValue);
                    break;
            }

            allocations = SortAllocationItems(allocations, sortBy, sortType);

            if (pageSize < 0)
            {
                return allocations;
            }
            return allocations.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        private IQueryable<ProjectAllocationDto> SortAllocationItems(IQueryable<ProjectAllocationDto> allocations, string sortBy, string sortType)
        {
            switch (sortBy?.ToLower())
            {
                case "pname":
                    allocations = sortType == "asc" ? allocations.OrderBy(a => a.ProjectName) :
                        allocations.OrderByDescending(a => a.ProjectName);
                    break;
                case "pmname":
                    allocations = sortType == "asc" ? allocations.OrderBy(a => a.ProjectName) :
                        allocations.OrderByDescending(a => a.ProjectManagerName);
                    break;
                case "accname":
                    allocations = sortType == "asc" ? allocations.OrderBy(a => a.AccountName) :
                        allocations.OrderByDescending(a => a.AccountName);
                    break;
                case "altype":
                    allocations = sortType == "asc" ? allocations.OrderBy(a => a.AllocationTypeName) :
                        allocations.OrderByDescending(a => a.AllocationTypeName);
                    break;
                case "percent":
                    allocations = sortType == "asc" ? allocations.OrderBy(a => a.PercentageOfAllocation) :
                        allocations.OrderByDescending(a => a.PercentageOfAllocation);
                    break;
                case "sdate":
                    allocations = sortType == "asc" ? allocations.OrderBy(a => a.AllocationStartDate) :
                        allocations.OrderByDescending(a => a.AllocationStartDate);
                    break;
                case "edate":
                    allocations = sortType == "asc" ? allocations.OrderBy(a => a.AllocationEndDate) :
                       allocations.OrderByDescending(a => a.AllocationEndDate);
                    break;
                default:
                    allocations = sortType == "asc" ? allocations.OrderBy(a => a.EmployeeName) :
                        allocations.OrderByDescending(a => a.EmployeeName);
                    break;
            }

            return allocations;
        }

        private IQueryable<ProjectAllocationDto> GetAllAllocationHistory()
        {
            return from p in Entities
                   join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                   from emd in eme.DefaultIfEmpty()
                   join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                   from scd in sce.DefaultIfEmpty()
                   join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                   from prd in pre.DefaultIfEmpty()
                   join pm in DataContext.Employees on prd.ProjectManagerID equals pm.EmployeeEntryID into pme
                   from pmd in pme.DefaultIfEmpty()
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false && p.AllocationEndDate < DateTime.Now
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.FirstName + " " + emd.LastName,
                       ProjectManagerName = pmd.FirstName + " " + pmd.LastName,
                       EmployeeID = p.EmployeeID,
                       ProjectName = prd.ProjectName,
                       Remarks = p.Remarks,
                       PercentageOfAllocation = p.PercentageOfAllocation,
                       AccountName = acd.AccountName
                   };
        }

        private IQueryable<ProjectAllocationDto> GetAllAllocationHistoryByProject(int projectID)
        {
            return from p in Entities
                   join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                   from emd in eme.DefaultIfEmpty()
                   join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                   from scd in sce.DefaultIfEmpty()
                   join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                   from prd in pre.DefaultIfEmpty()
                   join pm in DataContext.Employees on prd.ProjectManagerID equals pm.EmployeeEntryID into pme
                   from pmd in pme.DefaultIfEmpty()
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false
                        && p.AllocationEndDate < DateTime.Now
                        && p.ProjectID == projectID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.FirstName + " " + emd.LastName,
                       ProjectManagerName = pmd.FirstName + " " + pmd.LastName,
                       EmployeeID = p.EmployeeID,
                       ProjectName = prd.ProjectName,
                       Remarks = p.Remarks,
                       PercentageOfAllocation = p.PercentageOfAllocation,
                       AccountName = acd.AccountName
                   };



        }

        private IQueryable<ProjectAllocationDto> GetAllAllocationHistoryByManagerID(int managerID)
        {
            return from p in Entities
                   join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                   from emd in eme.DefaultIfEmpty()
                   join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                   from scd in sce.DefaultIfEmpty()
                   join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                   from prd in pre.DefaultIfEmpty()
                   join pm in DataContext.Employees on prd.ProjectManagerID equals pm.EmployeeEntryID into pme
                   from pmd in pme.DefaultIfEmpty()
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false && p.AllocationEndDate < DateTime.Now && prd.ProjectManagerID == managerID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.FirstName + " " + emd.LastName,
                       ProjectManagerName = pmd.FirstName + " " + pmd.LastName,
                       EmployeeID = p.EmployeeID,
                       ProjectName = prd.ProjectName,
                       Remarks = p.Remarks,
                       PercentageOfAllocation = p.PercentageOfAllocation,
                       AccountName = acd.AccountName
                   };



        }

        private IQueryable<ProjectAllocationDto> GetAllAllocationHistoryByEmployeeID(int employeeID)
        {
            return from p in Entities
                   join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                   from emd in eme.DefaultIfEmpty()
                   join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                   from scd in sce.DefaultIfEmpty()
                   join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                   from prd in pre.DefaultIfEmpty()
                   join pm in DataContext.Employees on prd.ProjectManagerID equals pm.EmployeeEntryID into pme
                   from pmd in pme.DefaultIfEmpty()
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false && p.AllocationEndDate < DateTime.Now && p.EmployeeID == employeeID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.FirstName + " " + emd.LastName,
                       ProjectManagerName = pmd.FirstName + " " + pmd.LastName,
                       EmployeeID = p.EmployeeID,
                       ProjectName = prd.ProjectName,
                       Remarks = p.Remarks,
                       PercentageOfAllocation = p.PercentageOfAllocation,
                       AccountName = acd.AccountName
                   };
        }

        public override int TotalRecordsCount()
        {
            return Entities.Count(e => e.IsDeleted == false && e.AllocationEndDate >= DateTime.Now);
        }

        public int TotalRecordsCount(string filterType, int filterValueID)
        {
            if (string.IsNullOrWhiteSpace(filterType))
            {
                return TotalRecordsCount();
            }

            int count = 0;
            switch (filterType)
            {
                case "emp":
                    count = GetAllocationsByEmployeeID(filterValueID).Count();
                    break;
                case "prj":
                    count = GetAllocationsByProjectID(filterValueID).Count();
                    break;
                case "pm":
                    count = GetAllocationsByProjectManagerID(filterValueID).Count();
                    break;
            }

            return count;
        }

        public bool AnyActiveBillableAllocations(int employeeID, int allocationID)
        {
            return (from a in Entities
                    join p in DataContext.Projects on a.ProjectID equals p.ProjectID
                    where a.IsDeleted == false && a.AllocationEndDate >= DateTime.Now
                        && p.ProjectName.ToLower() != "bench"
                        && a.EmployeeID == employeeID
                        && a.AllocationEntryID != allocationID
                    select a).Any();
        }

        public bool AnyActiveAllocationInBenchProject(int employeeID)
        {
            return (from a in Entities
                    join p in DataContext.Projects on a.ProjectID equals p.ProjectID
                    where a.IsDeleted == false
                        && p.ProjectName.ToLower().Contains("bench")
                        && a.EmployeeID == employeeID && a.AllocationEndDate >= DateTime.Now
                    select a).Any();
        }

        public void EndAllocation(int allocationID)
        {
            ProjectAllocation buzEntity = Entities.FirstOrDefault(e => e.AllocationEntryID == allocationID);
            buzEntity.AllocationEndDate = DateTime.Now.AddDays(-1);
            buzEntity.IsActive = false;
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public IEnumerable<ManagerWiseAllocationDto> GetManagerWiseAllocationSummary()
        {
            if (DataContext.IsPostgresDB)
            {
                return postgresSqlProcessor.GetManagerWiseAllocationSummaryFromPostgres();
            }

            DbCommand cmd = DataContext.Database.Connection.CreateCommand();
            cmd.CommandText = "dbo.GetManagerWiseProjectsSummary";
            cmd.CommandType = CommandType.StoredProcedure;
            DataContext.Database.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader();
            ObjectResult<ManagerWiseAllocationDto> items = ((IObjectContextAdapter)DataContext).ObjectContext.Translate<ManagerWiseAllocationDto>(reader);
            List<ManagerWiseAllocationDto> listItems = items.ToList();
            DataContext.Database.Connection.Close();
            return listItems;
        }

        public IEnumerable<BillabilityWiseAllocationSummaryDto> GetBillabilityWiseAllocationSummary()
        {
#pragma warning disable IDE0028 // Simplify collection initialization
            List<BillabilityWiseAllocationSummaryDto> items = new List<BillabilityWiseAllocationSummaryDto>();
#pragma warning restore IDE0028 // Simplify collection initialization

            items.Add(new BillabilityWiseAllocationSummaryDto
            {
                AllocationType = "Billable",
                AllocationTypeID = (int)AllocationType.Billable,
                NumberOfEmployees = GetEmployeesCountByAllocationType(AllocationType.Billable)
            });

            items.Add(new BillabilityWiseAllocationSummaryDto
            {
                AllocationType = "Committed Buffer",
                AllocationTypeID = (int)AllocationType.CommittedBuffer,
                NumberOfEmployees = GetEmployeesCountByAllocationType(AllocationType.CommittedBuffer)
            });

            items.Add(new BillabilityWiseAllocationSummaryDto
            {
                AllocationType = "Non-Committed Buffer",
                AllocationTypeID = (int)AllocationType.NonCommittedBuffer,
                NumberOfEmployees = GetEmployeesCountByAllocationType(AllocationType.NonCommittedBuffer)
            });

            items.Add(new BillabilityWiseAllocationSummaryDto
            {
                AllocationType = "Not Allocated - Delivery",
                AllocationTypeID = -1,
                NumberOfEmployees = DataContext.IsPostgresDB ? postgresSqlProcessor.GetNonAllocatedResourcesCountFromPostgres(true) : GetNonAllocatedResourcesCount(true),
            });

            items.Add(new BillabilityWiseAllocationSummaryDto
            {
                AllocationType = "BD & BO",
                AllocationTypeID = -2,
                NumberOfEmployees = DataContext.IsPostgresDB ? postgresSqlProcessor.GetNonAllocatedResourcesCountFromPostgres(false) : GetNonAllocatedResourcesCount(false),
            });

            return items;
        }

        public IEnumerable<BillabilityWiseAllocationDetailDto> GetBillabilityWiseAllocationDetail(string filterBy, string filterValue)
        {
            IEnumerable<BillabilityWiseAllocationDetailDto> allocationDetailDtos = null;

            switch (filterBy?.ToLower())
            {
                case "emp":
                case "psk":
                case "pod":
                case "ssk":
                case "all":
                    // filter by employee name/ primary skills/ secondary skills /pod
                    allocationDetailDtos = GetAllAllocationDetailFilteredByEmployeeData(filterBy, filterValue);
                    break;
                case "prj":
                case "acc":
                    // filter by project's project name/account
                    allocationDetailDtos = GetAllAllocationDetailFilteredByProjectData(filterBy, filterValue);
                    break;
                case "alt":
                    // filter by allocation type
                    int.TryParse(filterValue, out int allocationTypeID);
                    if (DataContext.IsPostgresDB)
                    {
                        allocationDetailDtos = postgresSqlProcessor.GetAllocationEntriesByAllocationTypeFromPostgres(allocationTypeID);
                    }
                    else
                    {
                        allocationDetailDtos = GetAllocationEntriesByAllocationType(allocationTypeID);
                    }
                    break;
            }

            return allocationDetailDtos;
        }

        public IEnumerable<UtilizedDaysSummaryDto> GetUtilizedDaysSummary(string filterBy, string filterValue, string sortBy, string sortType)
        {
            return PrepareUtilizedDaysSummary(filterBy, filterValue, sortBy, sortType);
        }

        private List<UtilizedDaysSummaryDto> PrepareUtilizedDaysSummary(string filterBy, string filterValue, string sortBy, string sortType)
        {
            List<UtilizedDaysSummaryDto> entries = new List<UtilizedDaysSummaryDto>();

            List<Employee> employees = (from e in DataContext.Employees
                                        where e.BusinessUnitID == 3 && e.IsDeleted == false && e.LastWorkingDay.HasValue == false
                                        || (e.LastWorkingDay.HasValue == true && e.LastWorkingDay.Value >= DateTime.Now)
                                        select e).ToList();

            foreach (Employee emp in employees)
            {
                UtilizedDaysSummaryDto entry = new UtilizedDaysSummaryDto
                {
                    EmployeeEntryID = emp.EmployeeEntryID,
                    DateOfJoin = emp.DateOfJoin,
                    EmployeeID = emp.EmployeeID,
                    EmployeeName = emp.FirstName + " " + emp.LastName,
                    PracticeID = emp.PracticeID,
                    PracticeName = DataContext.Practices.FirstOrDefault(p => p.PracticeID == emp.PracticeID)?.PracticeName,
                    AgingDays = 0
                };

                if (Entities.Count(a => a.IsDeleted == false && a.EmployeeID == emp.EmployeeEntryID) == 0)
                {
                    entry.AgingDays = DateTime.Now.Subtract(emp.DateOfJoin).Days;
                    entry.AnyAllocation = "No Allocations";
                    entries.Add(entry);
                }
                else if (Entities.Count(a => a.IsDeleted == false && a.EmployeeID == emp.EmployeeEntryID && a.AllocationEndDate <= DateTime.Now) > 0)
                {
                    DateTime? allocationEndDate = Entities.Where(a => a.IsDeleted == false
                        && a.EmployeeID == emp.EmployeeEntryID
                        && a.AllocationEndDate <= DateTime.Now).OrderByDescending(a => a.AllocationEndDate).Take(1).FirstOrDefault()?.AllocationEndDate;
                    entry.AnyAllocation = $"{Entities.Count(a => a.IsDeleted == false && a.EmployeeID == emp.EmployeeEntryID && a.AllocationEndDate <= DateTime.Now)} Allocation(s)";

                    if (allocationEndDate.HasValue)
                    {
                        entry.LastAllocatedDate = allocationEndDate;
                        entry.AgingDays = DateTime.Now.Subtract(allocationEndDate.Value).Days;
                    }
                    entries.Add(entry);
                }
                else
                {
                    // ignore the employee as he has active allocation
                }
            }

            sortType = string.IsNullOrEmpty(sortType) ? "asc" : sortType.ToLower();
            switch (sortBy?.ToLower())
            {
                case "aged":
                    entries = sortType == "asc" ? entries.OrderBy(o => o.AgingDays).ToList() : entries.OrderByDescending(o => o.AgingDays).ToList();
                    break;
                case "pod":
                    entries = sortType == "asc" ? entries.OrderBy(o => o.PracticeID).ToList() : entries.OrderByDescending(o => o.PracticeID).ToList();
                    break;
                default:
                    entries = sortType == "asc" ? entries.OrderBy(o => o.EmployeeName).ToList() : entries.OrderByDescending(o => o.EmployeeName).ToList();
                    break;
            }

            return entries;
        }

        #endregion

        #region Private Methods

        private List<BillabilityWiseAllocationDetailDto> GetAllocationEntriesByAllocationType(int allocationType)
        {
            List<BillabilityWiseAllocationDetailDto> allocationDetailDtos = null;
            DbCommand cmd = DataContext.Database.Connection.CreateCommand();
            cmd.CommandText = "dbo.GetBillabilityWiseDetails";
            SqlParameter param = new SqlParameter
            {
                ParameterName = "AllocationType",
                Value = allocationType
            };
            cmd.Parameters.Add(param);
            cmd.CommandType = CommandType.StoredProcedure;
            DataContext.Database.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader();
            ObjectResult<BillabilityWiseAllocationDetailDto> items = ((IObjectContextAdapter)DataContext).ObjectContext.Translate<BillabilityWiseAllocationDetailDto>(reader);
            allocationDetailDtos = items.ToList();
            DataContext.Database.Connection.Close();
            return allocationDetailDtos;
        }

        private ProjectAllocation CreateBusinessEntity(ProjectAllocationDto projectDto, bool isNewEntity = false)
        {
            ProjectAllocation entity = new ProjectAllocation
            {
                AllocationEndDate = new DateTime(projectDto.AllocationEndDate.Year, projectDto.AllocationEndDate.Month, projectDto.AllocationEndDate.Day),
                AllocationStartDate = new DateTime(projectDto.AllocationStartDate.Year, projectDto.AllocationStartDate.Month, projectDto.AllocationStartDate.Day),
                AllocationTypeID = projectDto.AllocationTypeID,
                EmployeeID = projectDto.EmployeeID,
                ProjectID = projectDto.ProjectID,
                PercentageOfAllocation = projectDto.PercentageOfAllocation,
                AllocationEntryID = projectDto.AllocationEntryID,
                IsActive = projectDto.AllocationEndDate > DateTime.Now,
                Remarks = projectDto.Remarks,
            };

            entity.UpdateTimeStamp(projectDto.LoggedInUserName, isNewEntity: true);
            return entity;
        }

        private void MigrateEntity(ProjectAllocationDto sourceEntity, ProjectAllocation targetEntity)
        {
            targetEntity.AllocationEndDate = new DateTime(sourceEntity.AllocationEndDate.Year, sourceEntity.AllocationEndDate.Month, sourceEntity.AllocationEndDate.Day);
            targetEntity.AllocationStartDate = new DateTime(sourceEntity.AllocationStartDate.Year, sourceEntity.AllocationStartDate.Month, sourceEntity.AllocationStartDate.Day);
            targetEntity.AllocationTypeID = sourceEntity.AllocationTypeID;
            targetEntity.EmployeeID = sourceEntity.EmployeeID;
            targetEntity.ProjectID = sourceEntity.ProjectID;
            targetEntity.PercentageOfAllocation = sourceEntity.PercentageOfAllocation;
            targetEntity.Remarks = sourceEntity.Remarks;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }

        private int GetEmployeesCountByAllocationType(AllocationType allocationType)
        {
            return (from e in DataContext.Employees
                    join a in Entities on e.EmployeeEntryID equals a.EmployeeID
                    where a.AllocationTypeID == (int)allocationType
                    && e.IsDeleted == false && a.IsDeleted == false
                    && a.AllocationEndDate >= DateTime.Now
                    && (e.LastWorkingDay.HasValue == false || (e.LastWorkingDay.HasValue == true && e.LastWorkingDay.Value >= DateTime.Now))
                    select a.EmployeeID).Distinct().Count();
        }

        private List<BillabilityWiseAllocationDetailDto> GetAllAllocationDetailFilteredByProjectData(string filterBy, string filterValue)
        {
            List<BillabilityWiseAllocationDetailDto> allocationDetails = new List<BillabilityWiseAllocationDetailDto>();
            List<Project> projects = null;
            string filterByText = filterBy?.ToLower();
            int.TryParse(filterValue, out int filterValueID);

            if (filterByText == "prj")
            {
                projects = DataContext.Projects.Where(p => p.IsDeleted == false && p.ProjectID == filterValueID).ToList();
            }
            else if (filterByText == "acc")
            {
                projects = DataContext.Projects.Where(p => p.IsDeleted == false && p.ProjectAccountID == filterValueID).ToList();
            }

            if (projects.Count == 0)
            {
                return allocationDetails;
            }

            foreach (Project project in projects)
            {
                List<ProjectAllocation> allocations = DataContext.ProjectAllocations.Where(a => a.IsDeleted == false
                    && a.AllocationEndDate >= DateTime.Now && a.ProjectID == project.ProjectID).ToList();
                foreach (ProjectAllocation allocation in allocations)
                {
                    Employee emp = DataContext.Employees.FirstOrDefault(e => e.EmployeeEntryID == allocation.EmployeeID);
                    allocationDetails.Add(new BillabilityWiseAllocationDetailDto
                    {
                        AllocationTypeID = allocation.AllocationTypeID,
                        AllocationType = DataContext.DropDownSubCategories.FirstOrDefault(ds => ds.SubCategoryID == allocation.AllocationTypeID)?.SubCategoryName,
                        EmployeeEntryID = emp.EmployeeEntryID,
                        EmployeeID = emp.EmployeeID,
                        EmployeeName = emp.FirstName + " " + emp.LastName,
                        PrimarySkills = emp.PrimarySkills,
                        SecondarySkills = emp.SecondarySkills,
                        AllocationEndDate = allocation.AllocationEndDate,
                        AllocationStartDate = allocation.AllocationStartDate,
                        BusinessUnitID = project.BusinessUnitID,
                        BusinessUnit = DataContext.DropDownSubCategories.FirstOrDefault(bu => bu.SubCategoryID == project.BusinessUnitID)?.SubCategoryName,
                        POD = DataContext.Practices.FirstOrDefault(p => p.PracticeID == project.PracticeID)?.PracticeName,
                        PracticeID = project.PracticeID,
                        ProjectID = project.ProjectID,
                        ProjectManager = (from e in DataContext.Employees where e.EmployeeEntryID == project.ProjectManagerID select e.FirstName + " " + e.LastName).FirstOrDefault(),
                        ProjectManagerID = project.ProjectManagerID,
                        ProjectName = project.ProjectName,
                        ProjectTypeID = project.ProjectTypeID,
                        ProjectType = DataContext.DropDownSubCategories.FirstOrDefault(bu => bu.SubCategoryID == project.ProjectTypeID)?.SubCategoryName,
                    });
                }
            }

            return allocationDetails;
        }

        private List<BillabilityWiseAllocationDetailDto> GetAllAllocationDetailFilteredByEmployeeData(string filterBy, string filterValue)
        {
            List<BillabilityWiseAllocationDetailDto> allocationDetails = new List<BillabilityWiseAllocationDetailDto>();
            List<Employee> employees = null;
            string filterByText = filterBy?.ToLower();

            switch (filterByText)
            {
                case "all":
                    employees = DataContext.Employees.Where(e => e.IsDeleted == false
                        && (e.LastWorkingDay.HasValue == false || (e.LastWorkingDay.HasValue == true && e.LastWorkingDay.Value >= DateTime.Now))).ToList();
                    break;
                case "emp":
                    int.TryParse(filterValue, out int empID);
                    employees = DataContext.Employees.Where(e => e.IsDeleted == false
                        && (e.LastWorkingDay.HasValue == false || (e.LastWorkingDay.HasValue == true && e.LastWorkingDay.Value >= DateTime.Now))
                        && e.EmployeeEntryID == empID).ToList();
                    break;
                case "psk":
                    employees = DataContext.Employees.Where(e => e.IsDeleted == false
                        && (e.LastWorkingDay.HasValue == false || (e.LastWorkingDay.HasValue == true && e.LastWorkingDay.Value >= DateTime.Now))
                        && e.PrimarySkills.ToLower().Contains(filterValue.ToLower())).ToList();
                    break;
                case "ssk":
                    employees = DataContext.Employees.Where(e => e.IsDeleted == false
                        && (e.LastWorkingDay.HasValue == false || (e.LastWorkingDay.HasValue == true && e.LastWorkingDay.Value >= DateTime.Now))
                        && e.SecondarySkills.ToLower().Contains(filterValue.ToLower())).ToList();
                    break;
                case "pod":
                    int.TryParse(filterValue, out int practiceID);
                    employees = DataContext.Employees.Where(e => e.IsDeleted == false
                        && (e.LastWorkingDay.HasValue == false || (e.LastWorkingDay.HasValue == true && e.LastWorkingDay.Value >= DateTime.Now))
                        && e.PracticeID == practiceID).ToList();
                    break;
            }

            if (employees.Count() == 0)
            {
                return allocationDetails;
            }

            foreach (Employee emp in employees)
            {
                List<ProjectAllocation> allocations = DataContext.ProjectAllocations.Where(a => a.IsDeleted == false
                    && a.AllocationEndDate >= DateTime.Now && a.EmployeeID == emp.EmployeeEntryID).ToList();

                // if no active allocations found, creating an allocation entry with allocation type as not allocated
                if (allocations.Count() == 0)
                {
                    allocationDetails.Add(new BillabilityWiseAllocationDetailDto
                    {
                        AllocationTypeID = 6,
                        AllocationType = "Not Allocated Yet", // non-comitted buffer
                        Comments = "No allocations found",
                        EmployeeEntryID = emp.EmployeeEntryID,
                        EmployeeID = emp.EmployeeID,
                        EmployeeName = emp.FirstName + " " + emp.LastName,
                        PrimarySkills = emp.PrimarySkills,
                        SecondarySkills = emp.SecondarySkills,
                        POD = DataContext.Practices.FirstOrDefault(p => p.PracticeID == emp.PracticeID)?.PracticeName,
                        PracticeID = emp.PracticeID,
                        ProjectManagerID = emp.ReportingManagerID,
                        ProjectManager = (from e in DataContext.Employees where e.EmployeeEntryID == emp.ReportingManagerID select e.FirstName + " " + e.LastName).FirstOrDefault(),
                        BusinessUnitID = emp.BusinessUnitID,
                        BusinessUnit = DataContext.DropDownSubCategories.FirstOrDefault(bu => bu.SubCategoryID == emp.BusinessUnitID)?.SubCategoryName
                    });
                }
                else
                {
                    foreach (ProjectAllocation allocation in allocations)
                    {
                        BillabilityWiseAllocationDetailDto allocationDetail = new BillabilityWiseAllocationDetailDto
                        {
                            AllocationTypeID = allocation.AllocationTypeID,
                            AllocationType = DataContext.DropDownSubCategories.FirstOrDefault(ds => ds.SubCategoryID == allocation.AllocationTypeID)?.SubCategoryName,
                            EmployeeEntryID = emp.EmployeeEntryID,
                            EmployeeID = emp.EmployeeID,
                            EmployeeName = emp.FirstName + " " + emp.LastName,
                            PrimarySkills = emp.PrimarySkills,
                            SecondarySkills = emp.SecondarySkills,
                            AllocationEndDate = allocation.AllocationEndDate,
                            AllocationStartDate = allocation.AllocationStartDate,
                            BusinessUnitID = emp.BusinessUnitID,
                            BusinessUnit = DataContext.DropDownSubCategories.FirstOrDefault(bu => bu.SubCategoryID == emp.BusinessUnitID)?.SubCategoryName,
                            POD = DataContext.Practices.FirstOrDefault(p => p.PracticeID == emp.PracticeID)?.PracticeName,
                            PracticeID = emp.PracticeID,
                        };

                        Project prj = DataContext.Projects.FirstOrDefault(p => p.ProjectID == allocation.ProjectID);
                        if (prj == null)
                        {
                            allocationDetail.Comments = "Project is missing";
                        }
                        else
                        {
                            allocationDetail.ProjectID = prj.ProjectID;
                            allocationDetail.ProjectManager = (from e in DataContext.Employees where e.EmployeeEntryID == prj.ProjectManagerID select e.FirstName + " " + e.LastName)?.FirstOrDefault();
                            allocationDetail.ProjectManagerID = prj.ProjectManagerID;
                            allocationDetail.ProjectName = prj.ProjectName;
                            allocationDetail.ProjectTypeID = prj.ProjectTypeID;
                            allocationDetail.ProjectType = DataContext.DropDownSubCategories.FirstOrDefault(bu => bu.SubCategoryID == prj.ProjectTypeID)?.SubCategoryName;
                            allocationDetail.AccountName = DataContext.ProjectAccounts.FirstOrDefault(a => a.AccountID == prj.ProjectAccountID)?.AccountName;
                        }

                        allocationDetails.Add(allocationDetail);
                    }
                }

            }
            return allocationDetails;
        }

        private int GetNonAllocatedResourcesCount(bool forDelivery = true)
        {
            DbCommand cmd = DataContext.Database.Connection.CreateCommand();
            cmd.CommandText = "dbo.GetCountOfNotAllocatedEmployees";
            SqlParameter param = new SqlParameter
            {
                ParameterName = "IsForDelivery",
                Value = forDelivery ? 1 : 0
            };
            cmd.Parameters.Add(param); cmd.CommandType = CommandType.StoredProcedure;
            DataContext.Database.Connection.Open();
            object result = cmd.ExecuteScalar();
            int count = result != null ? int.Parse(result.ToString()) : 0;
            DataContext.Database.Connection.Close();
            return count;
        }

        #endregion
    }

    public interface IAllocationRepository : IRepository<ProjectAllocationDto>
    {
        int Exists(int empEntryID, int projectID);

        int Exists(int allocationID, int empEntryID, int projectID);

        int GetPercentageOfAllocation(int employeeID);

        IEnumerable<CustomAllocationDto> GetAllocatedProjectsByEmployeeID(int employeeID);

        IEnumerable<ProjectAllocationDto> GetAllAllocationsByProjectID(int projectID);

        int GetTotalCountForAllocationHistory(string filterType, int filterValue);

        IEnumerable<ProjectAllocationDto> GetAllocationHistory(string filterType, int filterValue, string sortBy, string sortType, int pageSize = -1, int pageNo = -1);

        IEnumerable<ProjectAllocationDto> GetAll(string filterType, int filterValueID, string sortBy, string sortType, int pageSize = -1, int pageNo = -1);

        int TotalRecordsCount(string filterType, int filterValueID);

        bool AnyActiveBillableAllocations(int employeeID, int allocationID);

        bool AnyActiveAllocationInBenchProject(int employeeID);

        void EndAllocation(int allocationID);

        IEnumerable<ManagerWiseAllocationDto> GetManagerWiseAllocationSummary();

        IEnumerable<BillabilityWiseAllocationSummaryDto> GetBillabilityWiseAllocationSummary();

        IEnumerable<BillabilityWiseAllocationDetailDto> GetBillabilityWiseAllocationDetail(string filterBy, string filterValue);

        //IEnumerable<UtilizedDaysDetailDto> GetUtilizedDaysDetail(string filterBy, string filterValue, int pageSize = -1, int pageNo = -1);

        IEnumerable<UtilizedDaysSummaryDto> GetUtilizedDaysSummary(string filterBy, string filterValue, string sortBy, string sortType);
    }
}
