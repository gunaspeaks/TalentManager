using Agilisium.TalentManager.Service.Abstract;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDropDownCategoryService categoryService;
        private readonly IDropDownSubCategoryService subCategoryService;

        public AdminController(IDropDownCategoryService categoryService, IDropDownSubCategoryService subCategoryService)
        {
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}