using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class SubCategoryViewModel : ViewModelBase
    {
        public SubCategoryViewModel()
        {
            CategoryListItems = new List<SelectListItem>();
            SubCategories = new List<SubCategoryModel>();
        }

        public IEnumerable<SelectListItem> CategoryListItems { get; set; }

        public int SelectedCategoryID { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public IEnumerable<SubCategoryModel> SubCategories { get; set; }
    }
}