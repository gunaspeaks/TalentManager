using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Data.Entities
{
    public class DropDownCategory
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string ShortName { get; set; }

        public string Remarks { get; set; }
    }
}
