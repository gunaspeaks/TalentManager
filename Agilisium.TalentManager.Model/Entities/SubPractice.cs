using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Entities
{
    public class SubPractice : EntityBase
    {
        public int SubPracticeID { get; set; }

        public string SubPracticeName { get; set; }

        public string ShortName { get; set; }

        public int PracticeID { get; set; }

        public int? ManagerID { get; set; }
    }
}
