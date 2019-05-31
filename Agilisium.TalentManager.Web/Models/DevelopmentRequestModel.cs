using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class DevelopmentRequestModel: ViewModelBase
    {
        public DevelopmentRequestModel()
        {
            RequestedOn = DateTime.Now;
        }

        public int RequestID { get; set; }

        [DisplayName("Requested By")]
        public string RequestedBy { get; set; }

        [Required(ErrorMessage = "Please select a Request Type")]
        [DisplayName("Request Type")]
        public int RequestTypeID { get; set; }

        [DisplayName("Request Type")]
        public string RequestType { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Please enter Title")]
        public string RequestTitle { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("Priority")]
        [Required(ErrorMessage = "Please select a Priority")]
        public int PriorityID { get; set; }

        [DisplayName("Priority")]
        public string Priority { get; set; }

        [DisplayName("Requested On")]
        public DateTime RequestedOn { get; set; }

        [DisplayName("Request Status")]
        public int RequestStatusID { get; set; }

        [DisplayName("Request Status")]
        public string RequestStatus { get; set; }
    }
}