using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class CustomAllocationModel : ViewModelBase
    {
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("Project Code")]
        public string ProjectCode { get; set; }

        [DisplayName("Project Manager")]
        public string ProjectManager { get; set; }

        [DisplayName("Allocation Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("Allocation End Date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Allocated %")]
        public int AllocatedPercentage { get; set; }

        [DisplayName("Allocation Type")]
        public string UtilizatinType { get; set; }

        [DisplayName("Business Unit")]
        public string BusinessUnit { get; set; }

        [DisplayName("Practice")]
        public string Practice { get; set; }

        [DisplayName("Sub Practice")]
        public string SubPractice { get; set; }

        [DisplayName("Delivery Manager")]
        public string DeliveryManager { get; set; }
    }
}