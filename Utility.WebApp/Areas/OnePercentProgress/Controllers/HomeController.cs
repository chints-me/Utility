using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utility.WebApp.Areas.OnePercentProgress.Models;
using Utility.WebApp.Areas.OnePercentProgress.Services;
using Utility.WebApp.Helpers;
using Utility.WebApp.Models;
using Utility.WebApp.Repositories;

namespace Utility.WebApp.Areas.OnePercentProgress.Controllers
{
    [Area("OnePercentProgress")]
    public class HomeController : Controller
    {
        private readonly ProjectService projectService;

        public HomeController(ProjectService projectService)
        {
            this.projectService = projectService;
        }

        #region "Project"
        public async Task<IActionResult> Projects()
        {
            var items = await projectService.GetAll();
            return View(items);
        }

        public async Task<IActionResult> ViewProject(string id)
        {
            var item = await projectService.Get(id);
            return View(item);
        }

        public async Task<IActionResult> ManageProject(string id)
        {
            var item = await projectService.Get(id);
            return View(item);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ManageProject(ProjectViewModel viewModel)
        {
            if (!ModelState.IsValid) return Ok(UtilityHelper.ResponseAsJsonString(new ResponseViewModel { IsSuccess = false }));

            ResponseViewModel response = new ResponseViewModel();

            if (viewModel.Id.IsNullOrEmpty())
                response = await projectService.Insert(viewModel);
            else
                response = await projectService.Update(viewModel);

            return Ok(UtilityHelper.ResponseAsJsonString(response));
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
