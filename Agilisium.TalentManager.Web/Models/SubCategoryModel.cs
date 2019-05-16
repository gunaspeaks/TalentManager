using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class SubCategoryModel : ViewModelBase
    {
        public int SubCategoryID { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [DisplayName("Category Name")]
        public int CategoryID { get; set; }

        [DisplayName("Sub Category Name")]
        [Required(ErrorMessage ="Sub Category Name is required", AllowEmptyStrings =false)]
        [MaxLength(100, ErrorMessage = "Sub Category Name should not exceed 100 characters")]
        public string SubCategoryName { get; set; }

        [DisplayName("Short Name")]
        [Required(ErrorMessage = "Short Name is required", AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "Short Name should not exceed 10 characters")]
        public string ShortName { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "Description should not exceed 500 characters")]
        public string Description { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        public bool IsReserved { get; set; }
    }
}