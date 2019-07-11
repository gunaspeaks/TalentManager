using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class UtilizedDaysSummaryDto
    {
        public int EmployeeEntryID { get; set; }

        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public int PracticeID { get; set; }

        public string PracticeName { get; set; }

        public int AgingDays { get; set; }

        public DateTime DateOfJoin { get; set; }

        public string AnyAllocation { get; set; }

        public DateTime? LastAllocatedDate { get; set; }
    }
}
