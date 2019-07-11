using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class UtilizedDaysViewModel : ViewModelBase
    {
        public UtilizedDaysViewModel()
        {
            Employees = new List<UtilizedDaysSummaryModel>();
        }

        public IEnumerable<UtilizedDaysSummaryModel> Employees { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string FilterType { get; set; }

        public string FilterValue { get; set; }

        public List<SelectList> FilterValueListItems { get; set; }

        public string SortBy { get; set; }

        public string SortType { get; set; }
    }
}