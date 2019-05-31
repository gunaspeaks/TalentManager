using System;

namespace Agilisium.TalentManager.Dto
{
    public class DevelopmentRequestDto : DtoBase
    {
        public int RequestID { get; set; }

        public string RequestedBy { get; set; }

        public int RequestTypeID { get; set; }

        public string RequestType { get; set; }

        public string RequestTitle { get; set; }

        public string Remarks { get; set; }

        public int PriorityID { get; set; }

        public string Priority { get; set; }

        public DateTime RequestedOn { get; set; }

        public int RequestStatusID { get; set; }

        public string RequestStatus { get; set; }
    }
}
