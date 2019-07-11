using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class UtilizedDaysSummaryModel
    {
        public int EmployeeEntryID { get; set; }

        [DisplayName("Employee ID")]
        public string EmployeeID { get; set; }

        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }

        public int PracticeID { get; set; }

        [DisplayName("POD Name")]
        public string PracticeName { get; set; }

        [DisplayName("Last Allocation Age In Days")]
        public int AgingDays { get; set; }

        [DisplayName("Date of Join")]
        public DateTime DateOfJoin { get; set; }

        [DisplayName("Last Allocation End Date")]
        public DateTime? LastAllocatedDate { get; set; }

        [DisplayName("Number Of Allocations")]
        public string AnyAllocation { get; set; }
    }
}