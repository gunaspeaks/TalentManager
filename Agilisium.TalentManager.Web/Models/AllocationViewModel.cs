using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class AllocationViewModel : ViewModelBase
    {
        public IEnumerable<AllocationModel> Allocations { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public AllocationViewModel()
        {
            Allocations = new List<AllocationModel>();
            FilterTypeDropDownItems = new List<SelectListItem>();
            FilterValueDropDownItems = new List<SelectListItem>();
        }

        public string FilterType { get; set; }

        public string FilterValue { get; set; }

        public string SortBy { get; set; }

        public string SortType { get; set; }

        public IEnumerable<SelectListItem> FilterTypeDropDownItems { get; set; }

        public IEnumerable<SelectListItem> FilterValueDropDownItems { get; set; }
    }
}