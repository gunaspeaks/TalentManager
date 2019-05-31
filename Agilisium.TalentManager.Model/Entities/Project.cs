using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Model.Entities
{
    public class Project : EntityBase
    {
        [Key]
        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public string ProjectCode { get; set; }

        public int? DeliveryManagerID { get; set; }

        public int ProjectManagerID { get; set; }

        public int ProjectTypeID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Remarks { get; set; }

        public int BusinessUnitID { get; set; }

        public int PracticeID { get; set; }

        public int? SubPracticeID { get; set; }

        public bool IsSowAvailable { get; set; }

        public DateTime? SowStartDate { get; set; }

        public DateTime? SowEndDate { get; set; }

        public int ProjectAccountID { get; set; }

        [DefaultValue(false)]
        public bool IsReserved { get; set; }
    }
}
