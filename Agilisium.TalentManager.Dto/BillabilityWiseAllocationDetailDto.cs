using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class BillabilityWiseAllocationDetailDto
    {
        public int EmployeeEntryID { get; set; }

        public string EmployeeName { get; set; }

        public string ProjectName { get; set; }

        public string ProjectType { get; set; }

        public int ProjectID { get; set; }

        public string AccountName { get; set; }

        public string AllocationType { get; set; }

        public DateTime AllocationStartDate { get; set; }

        public DateTime AllocationEndDate { get; set; }
    }
}
