using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public abstract class ViewModelBase
    {
        public string LoggedInUserName { get; set; }

        public ViewModelBase()
        {
            LoggedInUserName = HttpContext.Current.User.Identity.Name;
        }
    }
}