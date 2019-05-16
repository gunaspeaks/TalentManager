using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class DropDownCategoryDto : DtoBase
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public bool IsReserved { get; set; }
    }
}
