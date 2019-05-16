using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class BillabilityWiseAllocationDetailModel
    {
        public int EmployeeEntryID { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Project Type")]
        public string ProjectType { get; set; }

        public int ProjectID { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Allocation Type")]
        public string AllocationType { get; set; }

        [Display(Name = "Allocation Start Date")]
        public DateTime AllocationStartDate { get; set; }

        [Display(Name = "Allocation End Date")]
        public DateTime AllocationEndDate { get; set; }
    }
}