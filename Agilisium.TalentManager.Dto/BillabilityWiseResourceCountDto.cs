using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class BillabilityWiseResourceCountDto
    {
        public int BillableCount { get; set; }

        public int CommittedBufferCount { get; set; }

        public int NonCommittedBufferCount { get; set; }
    }
}
