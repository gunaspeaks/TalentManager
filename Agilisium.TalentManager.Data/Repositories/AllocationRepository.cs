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
using System.Data.SqlClient;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class AllocationRepository : RepositoryBase<ProjectAllocation>, IAllocationRepository
    {
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
            IQueryable<ProjectAllocationDto> allocations = from p in Entities
                                                           join em in DataContext.Employees on p.EmployeeID equals em.EmployeeEntryID into eme
                                                           from emd in eme.DefaultIfEmpty()
                                                           join sc in DataContext.DropDownSubCategories on p.AllocationTypeID equals sc.SubCategoryID into sce
                                                           from scd in sce.DefaultIfEmpty()
                                                           join pr in DataContext.Projects on p.ProjectID equals pr.ProjectID into pre
                                                           from prd in pre.DefaultIfEmpty()
                                                           join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                                                           from acd in ace.DefaultIfEmpty()
                                                           where p.IsDeleted == false && p.IsActive == true
                                                           orderby prd.ProjectName, p.AllocationStartDate
                                                           select new ProjectAllocationDto
                                                           {
                                                               AllocationEndDate = p.AllocationEndDate,
                                                               AllocationEntryID = p.AllocationEntryID,
                                                               AllocationStartDate = p.AllocationStartDate,
                                                               AllocationTypeName = scd.SubCategoryName,
                                                               EmployeeName = emd.LastName + ", " + emd.FirstName,
                                                               EmployeeID = p.EmployeeID,
                                                               ProjectName = prd.ProjectName,
                                                               Remarks = p.Remarks,
                                                               PercentageOfAllocation = p.PercentageOfAllocation,
                                                               AccountName = acd.AccountName
                                                           };

            if (pageSize < 0)
            {
                return allocations;
            }
            return allocations.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<ProjectAllocationDto> GetAll(string filterType, int filterValueID, int pageSize = -1, int pageNo = -1)
        {
            if (string.IsNullOrWhiteSpace(filterType))
            {
                return GetAll(pageSize, pageNo);
            }

            IQueryable<ProjectAllocationDto> allocations = null;
            switch (filterType)
            {
                case "Employee":
                    allocations = GetAllocationsByEmployeeID(filterValueID);
                    break;
                case "Project":
                    allocations = GetAllocationsByProjectID(filterValueID);
                    break;
            }

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
                       EmployeeName = emd.LastName + ", " + emd.FirstName,
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
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false && p.IsActive == true && p.EmployeeID == empID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.LastName + ", " + emd.FirstName,
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
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false && p.IsActive == true && p.ProjectID == projectID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.LastName + ", " + emd.FirstName,
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
            if (Entities.Any(a => a.EmployeeID == employeeID && a.IsActive == true))
            {
                return Entities.Where(a => a.EmployeeID == employeeID && a.IsActive == true)
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
                    where a.EmployeeID == employeeID && a.IsActive == true
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
            if (string.IsNullOrWhiteSpace(filterType))
            {
                recordsCount = (from a in Entities
                                where a.IsDeleted == false
                                    && (a.IsActive == false || (a.IsActive == true && (a.AllocationEndDate.Year <= DateTime.Now.Year
                                    && a.AllocationEndDate.Month <= DateTime.Now.Month
                                    && a.AllocationEndDate.Day < DateTime.Now.Day)))
                                select a).Count();
            }

            switch (filterType)
            {
                case "Employee":
                    recordsCount = (from a in Entities
                                    where a.IsDeleted == false
                                        && (a.IsActive == false || (a.IsActive == true && (a.AllocationEndDate.Year <= DateTime.Now.Year
                                        && a.AllocationEndDate.Month <= DateTime.Now.Month
                                        && a.AllocationEndDate.Day < DateTime.Now.Day)))
                                        && a.EmployeeID == filterValue
                                    select a).Count();
                    break;
                case "Project":
                    recordsCount = (from a in Entities
                                    where a.IsDeleted == false
                                        && (a.IsActive == false || (a.IsActive == true && (a.AllocationEndDate.Year <= DateTime.Now.Year
                                        && a.AllocationEndDate.Month <= DateTime.Now.Month
                                        && a.AllocationEndDate.Day < DateTime.Now.Day)))
                                        && a.ProjectID == filterValue
                                    select a).Count();
                    break;
            }

            return recordsCount;
        }

        public IEnumerable<ProjectAllocationDto> GetAllocationHistory(string filterType, int filterValue, int pageSize = -1, int pageNo = -1)
        {
            IQueryable<ProjectAllocationDto> allocations = null;

            if (string.IsNullOrWhiteSpace(filterType))
            {
                allocations = GetAllAllocationHistory();
            }

            switch (filterType)
            {
                case "Employee":
                    allocations = GetAllAllocationHistoryByEmployeeID(filterValue);
                    break;
                case "Project":
                    allocations = GetAllAllocationHistoryByProject(filterValue);
                    break;
            }

            if (pageSize < 0)
            {
                return allocations;
            }
            return allocations.Skip((pageNo - 1) * pageSize).Take(pageSize);
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
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false
                        && (p.IsActive == false || (p.IsActive == true && (p.AllocationEndDate.Year <= DateTime.Now.Year
                        && p.AllocationEndDate.Month <= DateTime.Now.Month
                        && p.AllocationEndDate.Day < DateTime.Now.Day)))
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.LastName + ", " + emd.FirstName,
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
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false
                        && (p.IsActive == false || (p.IsActive == true && (p.AllocationEndDate.Year <= DateTime.Now.Year
                        && p.AllocationEndDate.Month <= DateTime.Now.Month
                        && p.AllocationEndDate.Day < DateTime.Now.Day)))
                        && p.ProjectID == projectID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.LastName + ", " + emd.FirstName,
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
                   join ac in DataContext.ProjectAccounts on prd.ProjectAccountID equals ac.AccountID into ace
                   from acd in ace.DefaultIfEmpty()
                   where p.IsDeleted == false
                        && (p.IsActive == false || (p.IsActive == true && (p.AllocationEndDate.Year <= DateTime.Now.Year
                        && p.AllocationEndDate.Month <= DateTime.Now.Month
                        && p.AllocationEndDate.Day < DateTime.Now.Day)))
                        && p.EmployeeID == employeeID
                   orderby prd.ProjectName, p.AllocationStartDate
                   select new ProjectAllocationDto
                   {
                       AllocationEndDate = p.AllocationEndDate,
                       AllocationEntryID = p.AllocationEntryID,
                       AllocationStartDate = p.AllocationStartDate,
                       AllocationTypeName = scd.SubCategoryName,
                       EmployeeName = emd.LastName + ", " + emd.FirstName,
                       EmployeeID = p.EmployeeID,
                       ProjectName = prd.ProjectName,
                       Remarks = p.Remarks,
                       PercentageOfAllocation = p.PercentageOfAllocation,
                       AccountName = acd.AccountName
                   };
        }

        public override int TotalRecordsCount()
        {
            return Entities.Count(e => e.IsDeleted == false && e.IsActive == true);
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
                case "Employee":
                    count = GetAllocationsByEmployeeID(filterValueID).Count();
                    break;
                case "Project":
                    count = GetAllocationsByProjectID(filterValueID).Count();
                    break;
            }

            return count;
        }

        public bool AnyActiveBillableAllocations(int employeeID, int allocationID)
        {
            return (from a in Entities
                    join p in DataContext.Projects on a.ProjectID equals p.ProjectID
                    where a.IsDeleted == false && a.IsActive == true
                        && p.ProjectName.ToLower() != "bench"
                        && a.EmployeeID == employeeID
                        && a.AllocationEntryID != allocationID
                    select a).Any();
        }

        public bool AnyActiveAllocationInBenchProject(int employeeID)
        {
            return (from a in Entities
                    join p in DataContext.Projects on a.ProjectID equals p.ProjectID
                    where a.IsDeleted == false && a.IsActive == true
                        && p.ProjectName.ToLower() == "bench"
                        && a.EmployeeID == employeeID
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
            DbCommand cmd = DataContext.Database.Connection.CreateCommand();
            cmd.CommandText = "dbo.GetBillabilityWiseSummary";
            cmd.CommandType = CommandType.StoredProcedure;
            DataContext.Database.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader();
            ObjectResult<BillabilityWiseAllocationSummaryDto> items = ((IObjectContextAdapter)DataContext).ObjectContext.Translate<BillabilityWiseAllocationSummaryDto>(reader);
            List<BillabilityWiseAllocationSummaryDto> listItems = items.ToList();
            DataContext.Database.Connection.Close();
            return listItems;
        }

        public IEnumerable<BillabilityWiseAllocationDetailDto> GetBillabilityWiseAllocationDetail(int allocationTypeID)
        {
            try
            {
                DbCommand cmd = DataContext.Database.Connection.CreateCommand();
                cmd.CommandText = "dbo.GetBillabilityWiseDetails";
                SqlParameter param = new SqlParameter
                {
                    ParameterName = "AllocationType",
                    Value = allocationTypeID
                };
                cmd.Parameters.Add(param);
                cmd.CommandType = CommandType.StoredProcedure;
                DataContext.Database.Connection.Open();
                DbDataReader reader = cmd.ExecuteReader();
                ObjectResult<BillabilityWiseAllocationDetailDto> items = ((IObjectContextAdapter)DataContext).ObjectContext.Translate<BillabilityWiseAllocationDetailDto>(reader);
                List<BillabilityWiseAllocationDetailDto> listItems = items.ToList();
                DataContext.Database.Connection.Close();
                return listItems;
            }
            catch (Exception)
            {

            }
            return null;
        }

        #endregion

        #region Private Methods

        private ProjectAllocation CreateBusinessEntity(ProjectAllocationDto projectDto, bool isNewEntity = false)
        {
            ProjectAllocation entity = new ProjectAllocation
            {
                AllocationEndDate = projectDto.AllocationEndDate,
                AllocationStartDate = projectDto.AllocationStartDate,
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
            targetEntity.AllocationEndDate = sourceEntity.AllocationEndDate;
            targetEntity.AllocationStartDate = sourceEntity.AllocationStartDate;
            targetEntity.AllocationTypeID = sourceEntity.AllocationTypeID;
            targetEntity.EmployeeID = sourceEntity.EmployeeID;
            targetEntity.ProjectID = sourceEntity.ProjectID;
            targetEntity.PercentageOfAllocation = sourceEntity.PercentageOfAllocation;
            targetEntity.Remarks = sourceEntity.Remarks;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
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

        IEnumerable<ProjectAllocationDto> GetAllocationHistory(string filterType, int filterValue, int pageSize = -1, int pageNo = -1);

        IEnumerable<ProjectAllocationDto> GetAll(string filterType, int filterValueID, int pageSize = -1, int pageNo = -1);

        int TotalRecordsCount(string filterType, int filterValueID);

        bool AnyActiveBillableAllocations(int employeeID, int allocationID);

        bool AnyActiveAllocationInBenchProject(int employeeID);

        void EndAllocation(int allocationID);

        IEnumerable<ManagerWiseAllocationDto> GetManagerWiseAllocationSummary();

        IEnumerable<BillabilityWiseAllocationSummaryDto> GetBillabilityWiseAllocationSummary();

        IEnumerable<BillabilityWiseAllocationDetailDto> GetBillabilityWiseAllocationDetail(int allocationTypeID);
    }
}
