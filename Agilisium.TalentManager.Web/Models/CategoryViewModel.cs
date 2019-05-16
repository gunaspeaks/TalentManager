using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class CategoryViewModel : ViewModelBase
    {
        public IEnumerable<CategoryModel> Categories { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public CategoryViewModel()
        {
            Categories = new List<CategoryModel>();
        }
    }
}