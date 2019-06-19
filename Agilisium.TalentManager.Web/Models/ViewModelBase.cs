using Agilisium.TalentManager.Web.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public abstract class ViewModelBase
    {
        public string LoggedInUserName { get; set; }

        public ViewModelBase()
        {
            StringBuilder adminName = new StringBuilder(HttpContext.Current.User.Identity.GetUserName());
            string ignorableString = HttpContext.Current.Application[UIConstants.CONFIG_IGNORABLE_TEXT_IN_USER_NAME]?.ToString();
            if (!string.IsNullOrWhiteSpace(ignorableString))
            {
                string[] spl = { "__" };
                string[] ignorableNames = ignorableString.Split(spl, StringSplitOptions.RemoveEmptyEntries);
                foreach (string name in ignorableNames)
                {
                    adminName.Replace(name, "");
                }
            }
            LoggedInUserName = adminName.ToString();
            HttpContext.Current.Application["LoggedInUser"] = LoggedInUserName;
        }
    }
}