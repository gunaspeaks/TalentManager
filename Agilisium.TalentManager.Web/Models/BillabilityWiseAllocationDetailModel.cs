using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class BillabilityWiseAllocationDetailModel
    {
        public int? EmployeeEntryID { get; set; }

        [Display(Name = "Employee ID")]
        public string EmployeeID { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Primary Skills")]
        public string PrimarySkills { get; set; }

        [Display(Name = "Secondary Skills")]
        public string SecondarySkills { get; set; }

        public int? BusinessUnitID { get; set; }

        [Display(Name = "Business Unit")]
        public string BusinessUnit { get; set; }

        public int? PracticeID { get; set; }

        [Display(Name = "POD")]
        public string POD { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Project Type")]
        public string ProjectType { get; set; }

        public int? ProjectID { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        public int? AllocationTypeID { get; set; }

        [Display(Name = "Allocation Type")]
        public string AllocationType { get; set; }

        [Display(Name = "Allocated From")]
        public DateTime? AllocationStartDate { get; set; }

        [Display(Name = "Allocated Upto")]
        public DateTime? AllocationEndDate { get; set; }

        public int? ProjectManagerID { get; set; }

        [Display(Name = "Project Manager")]
        public string ProjectManager { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }
    }
}