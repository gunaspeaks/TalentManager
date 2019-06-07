using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IAllocationService
    {
        void Add(ProjectAllocationDto entity);

        void Delete(ProjectAllocationDto entity);

        int Exists(int empEntryID, int projectID);

        int Exists(int allocationID, int empEntryID, int projectID);

        bool Exists(int id);

        bool Exists(string itemName);

        IEnumerable<ProjectAllocationDto> GetAll(int pageSize = -1, int pageNo = -1);

        ProjectAllocationDto GetByID(int id);

        void Update(ProjectAllocationDto entity);

        int GetPercentageOfAllocation(int employeeID);

        int TotalRecordsCount();

        IEnumerable<CustomAllocationDto> GetAllocatedProjectsByEmployeeID(int employeeID);

        int GetTotalRecordsCountForAllocationHistory(string filterType, int filterValue);

        List<ProjectAllocationDto> GetAllocationHistory(string filterType, int filterValue, string sortBy, string sortType, int pageSize = -1, int pageNo = -1);

        int TotalRecordsCount(string filterType, int filterValueID);

        List<ProjectAllocationDto> GetAll(string filterType, int filterValueID, string sortBy, string sortType, int pageSize = -1, int pageNo = -1);

        bool AnyActiveBillableAllocations(int employeeID, int allocationID);

        bool AnyActiveAllocationInBenchProject(int employeeID);

        void EndAllocation(int allocationID);

        List<ManagerWiseAllocationDto> GetManagerWiseAllocationSummary();

        List<ProjectAllocationDto> GetAllAllocationsByProjectID(int projectID);

        List<BillabilityWiseAllocationSummaryDto> GetBillabilityWiseAllocationSummary();

        List<BillabilityWiseAllocationDetailDto> GetBillabilityWiseAllocationDetail(string filterBy, string filterValue);
    }
}
