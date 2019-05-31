using System;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Model.Entities
{
    public class DevelopmentRequest : EntityBase
    {
        [Key]
        public int RequestID { get; set; }

        public string RequestedBy { get; set; }

        public int RequestTypeID { get; set; }

        public string RequestTitle { get; set; }

        public string Remarks { get; set; }

        public int PriorityID { get; set; }

        public DateTime RequestedOn { get; set; }

        public int RequestStatusID { get; set; }
    }
}
