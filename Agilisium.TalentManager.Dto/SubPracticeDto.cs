using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class SubPracticeDto : DtoBase
    {
        public int SubPracticeID { get; set; }

        public string SubPracticeName { get; set; }

        public string ShortName { get; set; }

        public int PracticeID { get; set; }

        public string PracticeName { get; set; }

        public int? ManagerID { get; set; }

        public string ManagerName { get; set; }

        public int HeadCount { get; set; }
    }
}
