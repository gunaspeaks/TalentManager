using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class BillabilityWiseResourceCountModel
    {
        [DisplayName("Billable")]
        public int BillableCount { get; set; }

        [DisplayName("Committed Buffer")]
        public int CommittedBufferCount { get; set; }

        [DisplayName("Non-Committed Buffer")]
        public int NonCommittedBufferCount { get; set; }
    }
}