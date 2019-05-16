using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class ServiceRequestDto : DtoBase
    {
        public int ServiceRequestID { get; set; }

        public int VendorID { get; set; }

        public DateTime RequestedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public string VendorName { get; set; }

        public string RequestedSkill { get; set; }

        public int RequestStatusID { get; set; }

        public string RequestStatus { get; set; }

        public string EmailMessage { get; set; }

        public string VendorEmailID { get; set; }

        public bool IsEmailSent { get; set; }
    }
}
