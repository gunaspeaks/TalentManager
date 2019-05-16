using Agilisium.TalentManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult GenerateMenu()
        {
            return PartialView(GetMenuItems(""));
        }

        private List<MenuItem> GetMenuItems()
        {
            List<MenuItem> menuBarItems = new List<MenuItem>
            {
                new MenuItem() { ID = 1, Action = "Index", ItemName = "File", Controller = "Employee", IsParent = true, ParentID = -1 },
                // drop down Menu 
                new MenuItem() { ID = 2, Action = "Index", ItemName = "Manage Resources", Controller = "Employee", IsParent = false, ParentID = 1 },
                new MenuItem() { ID = 3, Action = "Index", ItemName = "Manage Contractors", Controller = "Contractors", IsParent = false, ParentID = 1 },
                new MenuItem() { ID = 4, Action = "Index", ItemName = "Manager Vendors", Controller = "Vendors", IsParent = false, ParentID = 1 },

                new MenuItem() { ID = 5, Action = "Index", ItemName = "Projects", Controller = "Projects", IsParent = false, ParentID = -1 },
                new MenuItem() { ID = 6, Action = "Index", ItemName = "Project Allocations", Controller = "Allocations", IsParent = false, ParentID = -1 },

                new MenuItem() { ID = 7, Action = "Index", ItemName = "Reports", Controller = "Reports", IsParent = true, ParentID = -1 },
                new MenuItem() { ID = 8, Action = "Index", ItemName = "Allocation Summary", Controller = "Contractors", IsParent = false, ParentID = 7 },
                new MenuItem() { ID = 9, Action = "Index", ItemName = "Utlization Summary", Controller = "Contractors", IsParent = false, ParentID = 7 },

                new MenuItem() { ID = 15, Action = "Index", ItemName = "Application Settings", Controller = "Categories", IsParent = true, ParentID = -1 },
                new MenuItem() { ID = 16, Action = "Index", ItemName = "Categories", Controller = "Categories", IsParent = false, ParentID = 15 },
                new MenuItem() { ID = 17, Action = "Index", ItemName = "Sub Categories", Controller = "SubCategories", IsParent = false, ParentID = 15 },
                new MenuItem() { ID = 18, Action = "Index", ItemName = "Practices", Controller = "Practices", IsParent = false, ParentID = 15 },
                new MenuItem() { ID = 19, Action = "Index", ItemName = "Sub Practices", Controller = "SubPractices", IsParent = false, ParentID = 15 }
            };
            return menuBarItems;
        }

        private List<MenuItem> GetMenuItems(string sample)
        {
            var topNav = new List<MenuItem>();
            topNav.Add(new MenuItem() { ID = 1, Action = "About", ItemName = "About", Controller = "Home", IsParent = false, ParentID = -1 });
            topNav.Add(new MenuItem() { ID = 2, Action = "Contact", ItemName = "Contact", Controller = "Home", IsParent = false, ParentID = -1 });
            // drop down Menu 
            topNav.Add(new MenuItem() { ID = 3, Action = "Reports", ItemName = "Reports", Controller = "ReportGen", IsParent = true, ParentID = -1 });
            topNav.Add(new MenuItem() { ID = 4, Action = "SummaryReport", ItemName = "Overall Summary", Controller = "ReportGen", IsParent = false, ParentID = 3 });
            topNav.Add(new MenuItem() { ID = 5, Action = "DailyReport", ItemName = "Today Report", Controller = "ReportGen", IsParent = false, ParentID = 3 });
            topNav.Add(new MenuItem() { ID = 6, Action = "MonthlyReport", ItemName = "Month Report", Controller = "ReportGen", IsParent = false, ParentID = 3 });
            // End drop down Menu
            topNav.Add(new MenuItem() { ID = 7, Action = "Action", ItemName = "Other Action", Controller = "Home", IsParent = false, ParentID = -1 });
            return topNav;
        }
    }
}
