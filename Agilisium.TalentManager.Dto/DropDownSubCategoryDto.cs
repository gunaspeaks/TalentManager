using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class DropDownSubCategoryDto : DtoBase
    {
        public int SubCategoryID { get; set; }

        public string SubCategoryName { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public bool IsReserved { get; set; }
    }
}
