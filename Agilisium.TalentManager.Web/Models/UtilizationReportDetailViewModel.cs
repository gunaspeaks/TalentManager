using System.Collections.Generic;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class UtilizationReportDetailViewModel
    {
        public UtilizationReportDetailViewModel()
        {
            Allocations = new List<BillabilityWiseAllocationDetailModel>();

            FilterTypeListItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Employee Name",
                    Value = "emp"
                },
                new SelectListItem
                {
                    Text = "For All Resources",
                    Value = "all"
                },
                new SelectListItem
                {
                    Text = "POD",
                    Value = "pod"
                },
                new SelectListItem
                {
                    Text = "Project Name",
                    Value = "prj"
                },
                new SelectListItem
                {
                    Text = "Account Name",
                    Value = "acc"
                },
                new SelectListItem
                {
                    Text = "Allocation Type",
                    Value = "alt"
                },
            };

            FilterValueListItems = new List<SelectListItem>();
        }

        public List<BillabilityWiseAllocationDetailModel> Allocations { get; set; }

        public List<SelectListItem> FilterTypeListItems { get; set; }

        public List<SelectListItem> FilterValueListItems { get; set; }

        public string FilterType { get; set; }

        public string FilterValue { get; set; }
    }
}