using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class VisaHoldingEmployeeDto
    {
        public int EmployeeEntryID { get; set; }

        public string EmployeeID { get; set; }

        public string Practice { get; set; }

        public string ProjectName { get; set; }

        public string AllocationType { get; set; }

        public int AllocationID { get; set; }

        public string PrimarySkills { get; set; }

        public string SecondarySkills { get; set; }

        public string VisaType { get; set; }

        public string VisaInitiatedBy { get; set; }

        public DateTime VisaExpiryDate { get; set; }
    }
}
