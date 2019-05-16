using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class PracticeViewModel : ViewModelBase
    {
        public IEnumerable<PracticeModel> Practices { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public PracticeViewModel()
        {
            Practices = new List<PracticeModel>();
        }
    }
}