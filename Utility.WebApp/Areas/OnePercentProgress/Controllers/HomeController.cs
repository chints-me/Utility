using Microsoft.AspNetCore.Mvc;

namespace Utility.WebApp.Areas.OnePercentProgress.Controllers
{
    [Area("OnePercentProgress")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewProject(string id)
        {
            return View();
        }

        public IActionResult ManageProject(string id)
        {
            return View();
        }
    }
}
