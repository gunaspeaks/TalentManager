using System.Collections.Generic;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class ProjectViewModel : ViewModelBase
    {
        public IEnumerable<ProjectModel> Projects { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public ProjectViewModel()
        {
            Projects = new List<ProjectModel>();
            FilterTypeDropDownItems = new List<SelectListItem>();
            FilterValueDropDownItems = new List<SelectListItem>();
        }

        public string FilterType { get; set; }

        public string FilterValue { get; set; }

        public IEnumerable<SelectListItem> FilterTypeDropDownItems { get; set; }

        public IEnumerable<SelectListItem> FilterValueDropDownItems { get; set; }
    }
}