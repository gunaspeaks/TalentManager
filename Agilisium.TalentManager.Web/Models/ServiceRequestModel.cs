using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ServiceRequestModel : ViewModelBase
    {
        public int ServiceRequestID { get; set; }

        public int VendorID { get; set; }

        [DisplayName("Requested Date")]
        public DateTime RequestedDate { get; set; }

        [DisplayName("Completed Date")]
        public DateTime? CompletedDate { get; set; }

        [DisplayName("Vendor Name")]
        public string VendorName { get; set; }

        [DisplayName("Requested Skill")]
        public string RequestedSkill { get; set; }

        public int RequestStatusID { get; set; }

        [DisplayName("Request Status")]
        public string RequestStatus { get; set; }

        [DisplayName("Email Message")]
        public string EmailMessage { get; set; }

        public string VendorEmailID { get; set; }

        public bool IsEmailSent { get; set; }
    }
}