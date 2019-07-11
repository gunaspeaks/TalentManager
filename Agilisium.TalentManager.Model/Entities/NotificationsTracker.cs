using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Entities
{
    public class NotificationsTracker : EntityBase
    {
        [Key]
        public int TrackerID { get; set; }

        public int AllocationEntryID { get; set; }

        public DateTime? FirstAlertSentOn { get; set; }

        public DateTime? SecondAlertSentOn { get; set; }

        public DateTime? ThirdAlertSentOn { get; set; }
    }
}
