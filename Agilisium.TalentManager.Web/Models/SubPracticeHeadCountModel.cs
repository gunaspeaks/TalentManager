using System.Collections.Generic;
using System.ComponentModel;

namespace Agilisium.TalentManager.Web.Models
{
    public class SubPracticeHeadCountModel
    {
        public SubPracticeHeadCountModel()
        {
            SubPractices = new List<SubPracticeWiseCountModel>();
        }

        public int PracticeID { get; set; }

        [DisplayName("POD")]
        public string Practice { get; set; }

        [DisplayName("Head Count")]
        public int? HeadCount { get; set; }

        public List<SubPracticeWiseCountModel> SubPractices;
    }

    public class SubPracticeWiseCountModel
    {
        public int? SubPracticeID { get; set; }

        [DisplayName("Competency")]
        public string SubPractice { get; set; }

        [DisplayName("Head Count")]
        public int? HeadCount { get; set; }
    }
}