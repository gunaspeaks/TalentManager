using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class EmployeeWidgetModel
    {
        [DisplayName("Total Strength")]
        public int TotalEmployees { get; set; }

        [DisplayName("Billable Strength")]
        public int TotalBillableEmployees { get; set; }

        [DisplayName("Bench Strength")]
        public int BenchStrength { get; set; }

        [DisplayName("On Internal Projects")]
        public int EmployeesOnInternalProjects { get; set; }

        [DisplayName("Shadow Resources")]
        public int ShadowResources { get; set; }

        [DisplayName("On Internal Lab")]
        public int EmployeesOnLabProjects { get; set; }

        [DisplayName("Awaiting Proposal")]
        public int AwaitingProposal { get; set; }
    }
}