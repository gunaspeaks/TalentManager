using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Web.Models
{
    public class ContractorViewModel : ViewModelBase
    {
        public ContractorViewModel()
        {
            Contractors = new List<ContractorModel>();
        }

        public List<ContractorModel> Contractors { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }

    public class ContractorModel : ViewModelBase
    {
        public int ContractorID { get; set; }

        [DisplayName("Project")]
        [Required(ErrorMessage ="Please select a Project")]
        public int ProjectID { get; set; }

        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("Contractor Name")]
        [Required(ErrorMessage ="Contractor Name is required")]
        public string ContractorName { get; set; }

        public int AgilisiumManagerID { get; set; }

        [DisplayName("Agilisium Manager")]
        public string AgilisiumManagerName { get; set; }

        [DisplayName("Vendor")]
        public int VendorID { get; set; }

        [DisplayName("Vendor")]
        public string VendorName { get; set; }

        [Required(ErrorMessage ="Skillset is required")]
        public string SkillSet { get; set; }

        [DisplayName("Start Date")]
        [Required(ErrorMessage ="Start Date is required")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        [DisplayName("Billing Rate ($)")]
        [Required(ErrorMessage = "Billing Rate is required")]
        public double BillingRate { get; set; }

        [DisplayName("Client Rate ($)")]
        public double ClientRate { get; set; }

        [DisplayName("Onshore Rate ($)")]
        public double OnshoreRate { get; set; }

        [DisplayName("Contract Period")]
        [Required(ErrorMessage = "Please select a Contract Period")]
        public int ContractPeriodID { get; set; }

        [DisplayName("Contract Period")]
        public string ContractPeriod { get; set; }
    }
}