using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ManagerWiseAllocationModel
    {
        public int ProjectManagerID { get; set; }

        [DisplayName("Project Manager")]
        public string ManagerName { get; set; }

        [DisplayName("Projects Count")]
        public int ProjectCount { get; set; }
    }
}