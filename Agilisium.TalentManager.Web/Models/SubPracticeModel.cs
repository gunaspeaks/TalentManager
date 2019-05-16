using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class SubPracticeModel : ViewModelBase
    {
        [Required(ErrorMessage = "Please select a POD")]
        [DisplayName("POD Name")]
        public int PracticeID { get; set; }

        public int SubPracticeID { get; set; }

        [DisplayName("Competency")]
        [Required(ErrorMessage ="Competency is required", AllowEmptyStrings =false)]
        [MaxLength(100, ErrorMessage = "Competency should not exceed 100 characters")]
        public string SubPracticeName { get; set; }

        [DisplayName("Short Name")]
        [Required(ErrorMessage = "Short Name is required", AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "Short Name should not exceed 10 characters")]
        public string ShortName { get; set; }

        [DisplayName("POD Name")]
        public string PracticeName { get; set; }

        [DisplayName("Competency Manager")]
        public int? ManagerID { get; set; }

        [DisplayName("Competency Manager")]
        public string ManagerName { get; set; }

        [DisplayName("Head Count")]
        public int HeadCount { get; set; }
    }
}