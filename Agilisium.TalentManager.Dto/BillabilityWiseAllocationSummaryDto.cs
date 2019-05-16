using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class BillabilityWiseAllocationSummaryDto
    {
        public int AllocationTypeID { get; set; }

        public string AllocationType { get; set; }

        public int NumberOfEmployees { get; set; }
    }
}
