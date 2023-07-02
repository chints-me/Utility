using Utility.WebApp.Areas.OnePercentProgress.Models;
using Utility.WebApp.Models;
using Utility.WebApp.Repositories;
using Utility.WebApp.Helpers;
using Utility.WebApp.Enums;

namespace Utility.WebApp.Areas.OnePercentProgress.Services
{
    public class ProjectService
    {
        private readonly UnitOfWork unitOfWork;

        public ProjectService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResponseViewModel> Insert(ProjectViewModel viewModel)
        {
            ResponseViewModel response = new ResponseViewModel();

            var item = new Domains.OnePercentProgress.OPP_Project();
            item.Name = viewModel.Name;
            item = await unitOfWork.ProjectRepository.Add(item);

            await unitOfWork.Save();

            response.IsSuccess = true;
            response.RedirectUrl = "/OnePercentProgress/Home/Projects";
            return response;
        }

        public async Task<ResponseViewModel> Update(ProjectViewModel viewModel)
        {
            ResponseViewModel response = new ResponseViewModel();

            var item = await unitOfWork.ProjectRepository.Get(W => W.Id == viewModel.Id);
            item.Name = viewModel.Name;
            await unitOfWork.ProjectRepository.Update(item);

            await unitOfWork.Save();

            response.IsSuccess = true;
            response.RedirectUrl = "/OnePercentProgress/Home/Projects";
            return response;
        }

        public async Task<ProjectViewModel?> Get(string id)
        {
            Guid Id = id.TryParseGuidId();
            var item = await unitOfWork.ProjectRepository.Get(W => W.Id == Id);
            if (item != null)
            {
                ProjectViewModel viewModel = new ProjectViewModel();
                viewModel.Id = item.Id;
                viewModel.Name = item.Name;

                GetProjectCompletionStats(item.Id, out int totalTasks, out int totalTasksDone, out int completionPercentage);
                viewModel.TotalTasks = totalTasks;
                viewModel.TotalTasksDone = totalTasksDone;
                viewModel.CompletionPercentage = completionPercentage;

                return viewModel;
            }
            return null;
        }

        private void GetProjectCompletionStats(Guid projectId, out int totalTasks, out int totalTasksDone, out int completionPercentage)
        {
            var tasks = unitOfWork.TaskRepository.GetAll(W => W.OPP_ProjectId == projectId && W.OPP_ParentTaskId == Guid.Empty).Result;
            totalTasks = tasks.Count();
            totalTasksDone = tasks.Where(W => W.Status == TaskStatusEnum.Done.ToValue()).Count();
            completionPercentage = totalTasks > 0 ? (int)(((double)totalTasksDone / totalTasks) * 100) : 0;
        }

        public async Task<List<ProjectViewModel>> GetAll()
        {
            List<ProjectViewModel> viewModel = new List<ProjectViewModel>();

            var items = await unitOfWork.ProjectRepository.GetAll();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    GetProjectCompletionStats(item.Id, out int totalTasks, out int totalTasksDone, out int completionPercentage);
                    viewModel.Add(new ProjectViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        TotalTasks = totalTasks,
                        TotalTasksDone = totalTasksDone,
                        CompletionPercentage = completionPercentage,
                    });
                }
            }
            return viewModel;
        }
    }
}
