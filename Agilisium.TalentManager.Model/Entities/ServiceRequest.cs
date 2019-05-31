using System;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Model.Entities
{
    public class ServiceRequest : EntityBase
    {
        [Key]
        public int ServiceRequestID { get; set; }

        public int VendorID { get; set; }

        public DateTime RequestedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public string RequestedSkill { get; set; }

        public int RequestStatusID { get; set; }

        public string EmailMessage { get; set; }

        public bool IsEmailSent { get; set; }
    }
}
