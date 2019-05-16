using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class VendorSearchResultViewModel
    {
        public VendorSearchResultViewModel()
        {
            SearchResults = new List<VendorSearchModel>();
        }

        public IEnumerable<VendorSearchModel> SearchResults { get; set; }
    }

    public class VendorSearchModel
    {
        public VendorSearchModel()
        {
            MatchingVendors = new List<VendorModel>();
        }

        [DisplayName("Searched For")]
        public string SearchedFor { get; set; }

        public IEnumerable<VendorModel> MatchingVendors { get; set; }
    }
}