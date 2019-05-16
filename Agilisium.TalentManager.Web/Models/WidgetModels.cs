using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class AllocationsModel
    {
        public List<AllocationWidgetModel> TopAllocations { get; set; }
    }

    public class AllocationWidgetModel
    {
        public string ProjectName { get; set; }

        public int HeadCount { get; set; }
    }
}