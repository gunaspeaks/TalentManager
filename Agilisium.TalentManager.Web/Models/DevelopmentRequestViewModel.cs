using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Models
{
    public class DevelopmentRequestViewModel : ViewModelBase
    {
        public DevelopmentRequestViewModel()
        {
            DevelopmentRequests = new List<DevelopmentRequestModel>();
        }

        public IEnumerable<DevelopmentRequestModel> DevelopmentRequests { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}