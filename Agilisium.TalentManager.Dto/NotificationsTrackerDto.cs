using System;

namespace Agilisium.TalentManager.Dto
{
    public class NotificationsTrackerDto : DtoBase
    {
        public int TrackerID { get; set; }

        public int AllocationEntryID { get; set; }

        public DateTime? FirstAlertSentOn { get; set; }

        public DateTime? SecondAlertSentOn { get; set; }

        public DateTime? ThirdAlertSentOn { get; set; }
    }
}
