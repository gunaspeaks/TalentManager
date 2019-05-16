using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Entities
{
    public class Practice : EntityBase
    {
        public int PracticeID { get; set; }

        public string PracticeName { get; set; }

        public string ShortName { get; set; }

        public int? ManagerID { get; set; }

        public int BusinessUnitID { get; set; }

        public bool IsReserved { get; set; }
    }
}
