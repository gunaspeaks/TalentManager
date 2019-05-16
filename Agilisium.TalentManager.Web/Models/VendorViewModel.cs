using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Web.Models
{
    public class VendorViewModel : ViewModelBase
    {
        public VendorViewModel()
        {
            Vendors = new List<VendorModel>();
        }

        public List<VendorModel> Vendors { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string By { get; set; }
    }

    public class VendorModel : ViewModelBase
    {
        public int VendorID { get; set; }

        public bool IsSelected { get; set; }

        [DisplayName("Vendor Name")]
        [Required(ErrorMessage = "Please enter Vendor Name")]
        public string VendorName { get; set; }

        public string Location { get; set; }

        [DisplayName("Specialized Partner")]
        [Required(ErrorMessage = "Please select a Specialized Partner")]
        public int SpecializedPartnerID { get; set; }

        [DisplayName("Specialized Partner")]
        public string SpecializedPartner { get; set; }

        [DisplayName("CEO Name")]
        [Required(ErrorMessage = "Please enter CEO Name")]
        public string CEO { get; set; }

        [DisplayName("PoC 1")]
        [Required(ErrorMessage = "Please enter PoC Name for atleast one person")]
        public string PoC1 { get; set; }

        [DisplayName("Phone - PoC 1")]
        [Required(ErrorMessage = "Please enter Phone for PoC 1")]
        public string PoCPhone1 { get; set; }

        [DisplayName("Email - PoC 1")]
        [DataType(DataType.EmailAddress)]
        public string PoCEmail1 { get; set; }

        [DisplayName("PoC 2")]
        public string PoC2 { get; set; }

        [DisplayName("Phone - PoC 2")]
        public string PoCPhone2 { get; set; }

        [DisplayName("Email - PoC 2")]
        [DataType(DataType.EmailAddress)]
        public string PoCEmail2 { get; set; }

        [DisplayName("Primary Skills")]
        [Required(ErrorMessage = "Please enter Primary Skills")]
        public string PrimarySkills { get; set; }

        [DisplayName("Secondary Skills")]
        public string SecondarySkills { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public string RequestedSkill { get; set; }
    }
}