using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class EmployeeWidgetDto
    {
        public int TotalEmployees { get; set; }

        public int TotalBillableEmployees { get; set; }

        public int BenchStrength { get; set; }

        public int EmployeesOnInternalProjects { get; set; }

        public int ShadowResources { get; set; }

        public int EmployeesOnLabProjects { get; set; }

        public int AwaitingProposal { get; set; }
    }
}
