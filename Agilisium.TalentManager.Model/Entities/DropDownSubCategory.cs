using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agilisium.TalentManager.Model.Entities
{
    public class DropDownSubCategory : EntityBase
    {
        public int SubCategoryID { get; set; }

        public string SubCategoryName { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public bool IsReserved { get; set; }
    }
}
