using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Web.Models
{
    public class ProjectAccountModel : ViewModelBase
    {
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Account Name is required")]
        [DisplayName("Account Name")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Account Short Name is required")]
        [MaxLength(5, ErrorMessage = "Short Name can contain only 5 characters")]
        [DisplayName("Short Name")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "Onshore Manager Name is required")]
        [DisplayName("Onshore Manager")]
        public string OnshoreManager { get; set; }

        [Required(ErrorMessage = "Please select a Offshore Manager")]
        [DisplayName("Offshore Manager")]
        public int OffshoreManagerID { get; set; }

        [DisplayName("Offshore Manager")]
        public string OffshoreManager { get; set; }

        [DisplayName("Partner Manager")]
        public string PartnerManager { get; set; }

        [Required(ErrorMessage = "Please select a Country")]
        [DisplayName("Country")]
        public int CountryID { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; }
    }
}