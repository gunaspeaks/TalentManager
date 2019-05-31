using System;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Model.Entities
{
    public class Contractor : EntityBase
    {
        [Key]
        public int ContractorID { get; set; }

        public int ProjectID { get; set; }

        public string ContractorName { get; set; }

        public int AgilisiumManagerID { get; set; }

        public int VendorID { get; set; }

        public string SkillSet { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double BillingRate { get; set; }

        public double ClientRate { get; set; }

        public double OnshoreRate { get; set; }

        public int ContractPeriodID { get; set; }
    }
}
