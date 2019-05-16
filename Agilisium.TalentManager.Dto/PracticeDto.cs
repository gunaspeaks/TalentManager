using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class PracticeDto : DtoBase
    {
        public int PracticeID { get; set; }

        public string PracticeName { get; set; }

        public string ShortName { get; set; }

        public int? ManagerID { get; set; }

        public int BusinessUnitID { get; set; }

        public string  BusinessUnitName { get; set; }

        public string ManagerName { get; set; }

        public bool IsReserved { get; set; }

        public int HeadCount { get; set; }
    }
}
