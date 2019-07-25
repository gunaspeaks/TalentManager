using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class EmployeeDto : DtoBase
    {
        public int EmployeeEntryID { get; set; }

        public string EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailID { get; set; }

        public int BusinessUnitID { get; set; }

        public string BusinessUnitName { get; set; }

        public int PracticeID { get; set; }

        public string PracticeName { get; set; }

        public int SubPracticeID { get; set; }

        public string SubPracticeName { get; set; }

        public DateTime DateOfJoin { get; set; }

        public DateTime? LastWorkingDay { get; set; }

        public string PrimarySkills { get; set; }

        public string SecondarySkills { get; set; }

        public int? ReportingManagerID { get; set; }

        public string ReportingManagerName { get; set; }

        public string ProjectManagerName { get; set; }

        public int ProjectManagerID { get; set; }

        public int? UtilizationTypeID { get; set; }

        public string UtilizationTypeName { get; set; }

        public int EmploymentTypeID { get; set; }

        public string EmploymentTypeName { get; set; }

        public int? VisaCategoryID { get; set; }

        public string VisaCategory { get; set; }

        public DateTime? VisaValidUpto { get; set; }

        public int? TechnicalRank { get; set; }

        public float? TotalExperience { get; set; }

        public string PassportNo { get; set; }

        public DateTime? PassportValidUpto { get; set; }

        public string TravelledCountries { get; set; }
    }
}
