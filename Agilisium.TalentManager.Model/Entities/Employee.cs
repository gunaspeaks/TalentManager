using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Entities
{
    public class Employee : EntityBase
    {
        public int EmployeeEntryID { get; set; }

        public string EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailID { get; set; }

        public int BusinessUnitID { get; set; }

        public int PracticeID { get; set; }

        public int SubPracticeID { get; set; }

        public DateTime DateOfJoin { get; set; }

        public DateTime? LastWorkingDay { get; set; }

        public string PrimarySkills { get; set; }

        public string SecondarySkills { get; set; }

        public int? ReportingManagerID { get; set; }

        public int? UtilizationTypeID { get; set; }

        public int EmploymentTypeID { get; set; }
    }
}
