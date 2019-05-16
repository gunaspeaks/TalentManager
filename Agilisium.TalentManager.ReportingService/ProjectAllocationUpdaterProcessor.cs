using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.ReportingService
{
    public class ProjectAllocationUpdaterProcessor
    {
        private readonly IAllocationService allocationService;

        public ProjectAllocationUpdaterProcessor(IAllocationService allocationService)
        {
            this.allocationService = allocationService;
        }

        public void UpdateProjectAllocations()
        {
            List<ProjectAllocationDto> allocations = allocationService.GetAll().ToList();

            foreach (ProjectAllocationDto allocation in allocations)
            {
                if (allocation.AllocationEndDate.Year <= DateTime.Now.Year
                    && allocation.AllocationEndDate.Month <= DateTime.Now.Month
                    && allocation.AllocationEndDate.Day < DateTime.Now.Day)
                {
                    int employeeID = allocation.EmployeeID;
                }
            }
        }

        private void EndAllocation(ProjectAllocationDto allocation)
        {

        }

        private void AllocateEmployeeToBenchProject(int employeeID)
        {

        }
    }
}
