using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class PracticeHeadCountModel
    {
        public int PracticeID { get; set; }

        [DisplayName("POD")]
        public string Practice { get; set; }

        [DisplayName("Head Count")]
        public int HeadCount { get; set; }
    }
}