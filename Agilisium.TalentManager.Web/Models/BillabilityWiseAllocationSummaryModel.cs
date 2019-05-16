using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class BillabilityWiseAllocationSummaryModel
    {
        public int AllocationTypeID { get; set; }

        [Display(Name ="Allocation Type")]
        public string AllocationType { get; set; }

        [Display(Name = "Number of Employees")]
        public int NumberOfEmployees { get; set; }
    }
}