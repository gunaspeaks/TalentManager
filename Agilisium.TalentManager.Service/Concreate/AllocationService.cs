using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Service.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.Service.Concreate
{
    public class AllocationService : IAllocationService
    {
        private readonly IAllocationRepository repository;

        public AllocationService(IAllocationRepository repository)
        {
            this.repository = repository;
        }

        public void Add(ProjectAllocationDto entity)
        {
            repository.Add(entity);
        }

        public void Delete(ProjectAllocationDto entity)
        {
            repository.Delete(entity);
        }

        public int Exists(int empEntryID, int projectID)
        {
            return repository.Exists(empEntryID, projectID);
        }

        public int Exists(int allocationID, int empEntryID, int projectID)
        {
            return repository.Exists(allocationID, empEntryID, projectID);
        }

        public bool Exists(int id)
        {
            return repository.Exists(id);
        }

        public bool Exists(string itemName)
        {
            return repository.Exists(itemName);
        }

        public IEnumerable<ProjectAllocationDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(pageSize, pageNo);
        }

        public ProjectAllocationDto GetByID(int id)
        {
            return repository.GetByID(id);
        }

        public void Update(ProjectAllocationDto entity)
        {
            repository.Update(entity);
        }

        public int GetPercentageOfAllocation(int employeeID)
        {
            return repository.GetPercentageOfAllocation(employeeID);
        }

        public int TotalRecordsCount()
        {
            return repository.TotalRecordsCount();
        }

        public IEnumerable<CustomAllocationDto> GetAllocatedProjectsByEmployeeID(int employeeID)
        {
            return repository.GetAllocatedProjectsByEmployeeID(employeeID);
        }

        public List<ProjectAllocationDto> GetAllocationHistory(string filterType, int filterValue, string sortBy, string sortType, int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAllocationHistory(filterType, filterValue, sortBy, sortType, pageSize, pageNo).ToList();
        }

        public int GetTotalRecordsCountForAllocationHistory(string filterType, int filterValue)
        {
            return repository.GetTotalCountForAllocationHistory(filterType, filterValue);
        }

        public List<ProjectAllocationDto> GetAll(string filterType, int filterValueID, string sortBy, string sortType, int pageSize = -1, int pageNo = -1)
        {
            return repository.GetAll(filterType, filterValueID, sortBy, sortType, pageSize, pageNo).ToList();
        }

        public int TotalRecordsCount(string filterType, int filterValueID)
        {
            return repository.TotalRecordsCount(filterType, filterValueID);
        }

        public bool AnyActiveBillableAllocations(int employeeID, int allocationID)
        {
            return repository.AnyActiveBillableAllocations(employeeID, allocationID);
        }

        public bool AnyActiveAllocationInBenchProject(int employeeID)
        {
            return repository.AnyActiveAllocationInBenchProject(employeeID);
        }

        public void EndAllocation(int allocationID)
        {
            repository.EndAllocation(allocationID);
        }

        public List<ManagerWiseAllocationDto> GetManagerWiseAllocationSummary()
        {
            return repository.GetManagerWiseAllocationSummary().ToList();
        }

        public List<ProjectAllocationDto> GetAllAllocationsByProjectID(int projectID)
        {
            return repository.GetAllAllocationsByProjectID(projectID).ToList();
        }

        public List<BillabilityWiseAllocationSummaryDto> GetBillabilityWiseAllocationSummary()
        {
            return repository.GetBillabilityWiseAllocationSummary().ToList();
        }

        public List<BillabilityWiseAllocationDetailDto> GetBillabilityWiseAllocationDetail(string filterBy, string filterValue)
        {
            return repository.GetBillabilityWiseAllocationDetail(filterBy, filterValue).ToList();
        }
    }
}
