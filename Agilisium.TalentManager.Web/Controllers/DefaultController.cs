using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }
    }
}
