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
        private readonly TaskService taskService;

        public HomeController(ProjectService projectService, TaskService taskService)
        {
            this.projectService = projectService;
            this.taskService = taskService;
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
            if (item != null)
                return View(item);
            else
                return RedirectToAction("Projects", "Home", new { area = "OnePercentProgress" });
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

        public async Task<IActionResult> Tasks(string parentTaskId, string projectId)
        {
            TasksViewModel viewModel = new TasksViewModel();
            viewModel.ProjectId = projectId.TryParseGuidId();
            viewModel.ParentTaskId = parentTaskId.TryParseGuidId();
            if (viewModel.ProjectId == Guid.Empty && viewModel.ParentTaskId == Guid.Empty)
            {
                return RedirectToAction("Projects", "Home", new { area = "OnePercentProgress" });
            }

            viewModel.Tasks = await taskService.GetAll(viewModel.ParentTaskId, viewModel.ProjectId);
            var grandParentTask = await taskService.Get(viewModel.ParentTaskId);
            if(grandParentTask != null)
            {
                viewModel.GrandParentTaskId = grandParentTask.ParentTaskId;
            }
            return View(viewModel);
        }

        public async Task<IActionResult> ManageTask(string id, string parentTaskId, string projectId)
        {
            TaskViewModel viewModel = new TaskViewModel();
            viewModel.ProjectId = projectId.TryParseGuidId();
            viewModel.ParentTaskId = parentTaskId.TryParseGuidId();
            viewModel.Id = id.TryParseGuidId();
            var item = await taskService.Get(viewModel.Id.Value, viewModel.ParentTaskId, viewModel.ProjectId);
            return View(item);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ManageTask(TaskViewModel viewModel)
        {
            if (!ModelState.IsValid) return Ok(UtilityHelper.ResponseAsJsonString(new ResponseViewModel { IsSuccess = false }));

            ResponseViewModel response = new ResponseViewModel();

            if (viewModel.Id.IsNullOrEmpty())
                response = await taskService.Insert(viewModel);
            else
                response = await taskService.Update(viewModel);

            if (response.IsSuccess)
            {
                response.RedirectUrl = Url.Action("Tasks", "Home", new { area = "OnePercentProgress", parentTaskId = viewModel.ParentTaskId, projectId = viewModel.ProjectId });
            }

            return Ok(UtilityHelper.ResponseAsJsonString(response));
        }
        #endregion
    }
}
