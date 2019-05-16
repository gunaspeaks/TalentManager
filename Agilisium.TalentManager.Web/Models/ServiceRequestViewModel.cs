using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ServiceRequestViewModel : ViewModelBase
    {
        public IEnumerable<ServiceRequestModel> ServiceRequests { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public ServiceRequestViewModel()
        {
            ServiceRequests = new List<ServiceRequestModel>();
        }
    }
}