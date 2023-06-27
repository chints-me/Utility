using Microsoft.AspNetCore.Mvc;

namespace Utility.WebApp.Areas.OnePercentProgress.Controllers
{
    [Area("OnePercentProgress")]
    public class HomeController : Controller
    {
        #region "Project"
        public IActionResult Projects()
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
        #endregion

        #region Tasks

        public IActionResult Tasks(string id, string projectId)
        {
            return View();
        }

        public IActionResult ManageTask(string id, string projectId)
        {
            return View();
        }
        #endregion
    }
}
