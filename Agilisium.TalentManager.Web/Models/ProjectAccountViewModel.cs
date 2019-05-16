using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ProjectAccountViewModel : ViewModelBase
    {
        public IEnumerable<ProjectAccountModel> ProjectAccounts { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public ProjectAccountViewModel()
        {
            ProjectAccounts = new List<ProjectAccountModel>();
        }
    }
}