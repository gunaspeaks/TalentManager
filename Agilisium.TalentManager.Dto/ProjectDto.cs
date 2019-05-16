using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Dto
{
    public class ProjectDto : DtoBase
    {
        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public string ProjectCode { get; set; }

        public int? DeliveryManagerID { get; set; }

        public string DeliveryManagerName { get; set; }

        public int ProjectManagerID { get; set; }

        public string ProjectManagerName { get; set; }

        public int ProjectTypeID { get; set; }

        public string ProjectTypeName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Remarks { get; set; }

        public int BusinessUnitID { get; set; }

        public string BusinessUnitName { get; set; }

        public int PracticeID { get; set; }

        public string PracticeName { get; set; }

        public int? SubPracticeID { get; set; }

        public string SubPracticeName { get; set; }

        public bool IsSowAvailable { get; set; }

        public DateTime? SowStartDate { get; set; }

        public DateTime? SowEndDate { get; set; }

        public int ProjectAccountID { get; set; }

        public string AccountName { get; set; }

        public bool IsReserved { get; set; }
    }
}
