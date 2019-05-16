using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class ProjectModel : ViewModelBase
    {
        [DisplayName("Project ID")]
        public int ProjectID { get; set; }

        [DisplayName("Project Name")]
        [Required(ErrorMessage ="Project Name is required")]
        public string ProjectName { get; set; }

        [DisplayName("Project Code")]
        [Required(ErrorMessage ="Project Code is required")]
        public string ProjectCode { get; set; }

        [DisplayName("Delivery Manager")]
        public int? DeliveryManagerID { get; set; }

        [DisplayName("Delivery Manager")]
        public string DeliveryManagerName { get; set; }

        [DisplayName("Project Manager")]
        [Required(ErrorMessage ="Please select a Project Manager")]
        public int ProjectManagerID { get; set; }

        [DisplayName("Project Manager")]
        public string ProjectManagerName { get; set; }

        [DisplayName("Project Type")]
        [Required(ErrorMessage = "Please select a Project Type")]
        public int ProjectTypeID { get; set; }

        [DisplayName("Project Type")]
        public string ProjectTypeName { get; set; }

        [DisplayName("Start Date")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Project Start Date is required")]
        //[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "Project End Date is required")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [DisplayName("POD")]
        [Required(ErrorMessage = "Please select a POD")]
        public int PracticeID { get; set; }

        [DisplayName("POD")]
        public string PracticeName { get; set; }

        [DisplayName("Business Unit")]
        [Required(ErrorMessage = "Please select a Business Unit")]
        public int BusinessUnitID { get; set; }

        [DisplayName("Business Unit")]
        public string BusinessUnitName { get; set; }

        [DisplayName("Competence")]
        public int? SubPracticeID { get; set; }

        [DisplayName("Competence")]
        public string SubPracticeName { get; set; }

        [DisplayName("Is SoW Available")]
        public bool IsSowAvailable { get; set; }

        [DisplayName("SoW Start Date")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime? SowStartDate { get; set; }

        [DisplayName("SoW End Date")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime? SowEndDate { get; set; }

        [DisplayName("Account")]
        [Required(ErrorMessage = "Please select an Account")]
        public int ProjectAccountID { get; set; }

        [DisplayName("Account")]
        public string AccountName { get; set; }

        public bool IsReserved { get; set; }
    }
}