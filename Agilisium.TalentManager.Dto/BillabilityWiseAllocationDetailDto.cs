using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class BillabilityWiseAllocationDetailDto
    {
        public BillabilityWiseAllocationDetailDto()
        {
            EmployeeID = "";
            EmployeeName = "";
            PrimarySkills = "";
            SecondarySkills = "";
            BusinessUnit = "";
            POD = "";
            ProjectName = "";
        }

        public int? EmployeeEntryID { get; set; }

        public string EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public string PrimarySkills { get; set; }

        public string SecondarySkills { get; set; }

        public int? BusinessUnitID { get; set; }

        public string BusinessUnit { get; set; }

        public int? PracticeID { get; set; }

        public string POD { get; set; }

        public string ProjectName { get; set; }

        public int? ProjectTypeID { get; set; }

        public string ProjectType { get; set; }

        public int? ProjectID { get; set; }

        public string AccountName { get; set; }

        public int? AllocationTypeID { get; set; }

        public string AllocationType { get; set; }

        public DateTime? AllocationStartDate { get; set; }

        public DateTime? AllocationEndDate { get; set; }

        public int? ProjectManagerID { get; set; }

        public string ProjectManager { get; set; }

        public string Comments { get; set; }
    }
}
