using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class ProjectAllocationDto : DtoBase
    {
        public int AllocationEntryID { get; set; }

        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public DateTime AllocationStartDate { get; set; }

        public DateTime AllocationEndDate { get; set; }

        public int AllocationTypeID { get; set; }

        public string AllocationTypeName { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public string Remarks { get; set; }

        public int PercentageOfAllocation { get; set; }

        public string AccountName { get; set; }

        public bool IsActive { get; set; }
    }
}
