using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Data.Entities
{
    public class DropDownSubCategory
    {
        public int SubCategoryID { get; set; }

        public string SubCategoryName { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public DropDownCategory Category { get; set; }
    }
}
