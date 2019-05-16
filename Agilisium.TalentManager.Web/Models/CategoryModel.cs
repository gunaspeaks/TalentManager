using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agilisium.TalentManager.Web.Models
{
    public class CategoryModel : ViewModelBase
    {
        public int CategoryID { get; set; }

        [DisplayName("Category Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Name is required")]
        [MaxLength(100, ErrorMessage = "Category Name should not exceed 100 characters")]
        public string CategoryName { get; set; }

        [DisplayName("Short Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Short Name is required")]
        [MaxLength(10, ErrorMessage = "Short Name should not should not exceed 10 characters")]
        public string ShortName { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(100, ErrorMessage = "Description should not exceed 500 characters Name should not contain more than 100 characters")]
        public string Description { get; set; }

        public bool IsReserved { get; set; }
    }
}