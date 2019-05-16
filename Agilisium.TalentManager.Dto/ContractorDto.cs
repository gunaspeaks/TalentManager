using System;

namespace Agilisium.TalentManager.Dto
{
    public class ContractorDto : DtoBase
    {
        public int ContractorID { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public string ContractorName { get; set; }

        public int AgilisiumManagerID { get; set; }

        public string AgilisiumManagerName { get; set; }

        public int VendorID { get; set; }

        public string VendorName { get; set; }

        public string SkillSet { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double BillingRate { get; set; }

        public double ClientRate { get; set; }

        public double OnshoreRate { get; set; }

        public int ContractPeriodID { get; set; }

        public string ContractPeriod { get; set; }
    }
}
