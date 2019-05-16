using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class SubPracticeHeadCountDto
    {
        public int? PracticeID { get; set; }

        public int? SubPracticeID { get; set; }

        public string Practice { get; set; }

        public string SubPractice { get; set; }

        public int? HeadCount { get; set; }
    }
}
