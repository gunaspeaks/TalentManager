using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class MenuItem
    {
        public int ID { get; set; }

        public string ItemName { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public bool HavingImageClass { get; set; }

        public string CssClass { get; set; }

        public int ParentID { get; set; }

        public bool IsParent { get; set; }
    }
}