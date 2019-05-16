using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Model.Entities
{
    public class CustomAllocation
    {
        public string ProjectName { get; set; }

        public string ProjectCode { get; set; }

        public string ProjectManager { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int AllocatedPercentage { get; set; }

        public string UtilizatinType { get; set; }
    }
}
