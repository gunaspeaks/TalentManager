using System.Collections.Generic;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class SubPracticeViewModel : ViewModelBase
    {
        public SubPracticeViewModel()
        {
            PracticeListItems = new List<SelectListItem>();
            SubPractices = new List<SubPracticeModel>();
        }

        public IEnumerable<SelectListItem> PracticeListItems { get; set; }

        public int SelectedPracticeID { get; set; }

        public IEnumerable<SubPracticeModel> SubPractices { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}