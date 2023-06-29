using Utility.WebApp.Areas.OnePercentProgress.Models;
using Utility.WebApp.Helpers;
using Utility.WebApp.Models;
using Utility.WebApp.Repositories;

namespace Utility.WebApp.Areas.OnePercentProgress.Services
{
    public class TaskService
    {
        private readonly UnitOfWork unitOfWork;

        public TaskService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ResponseViewModel> Insert(TaskViewModel viewModel)
        {
            ResponseViewModel response = new ResponseViewModel();

            var item = new Domains.OnePercentProgress.OPP_Task();
            item.Name = viewModel.Name;
            item.ProjectId = viewModel.ProjectId;
            item = await unitOfWork.TaskRepository.Add(item);

            await unitOfWork.Save();

            response.IsSuccess = true;
            response.RedirectUrl = "/OnePercentProgress/Home/Tasks";
            return response;
        }

        public async Task<ResponseViewModel> Update(TaskViewModel viewModel)
        {
            ResponseViewModel response = new ResponseViewModel();

            var item = await unitOfWork.TaskRepository.Get(W => W.Id == viewModel.Id);
            item.Name = viewModel.Name;
            item.ProjectId = viewModel.ProjectId;
            await unitOfWork.TaskRepository.Update(item);

            await unitOfWork.Save();

            response.IsSuccess = true;
            response.RedirectUrl = "/OnePercentProgress/Home/Tasks";
            return response;
        }

        public async Task<TaskViewModel> Get(string id)
        {
            TaskViewModel viewModel = new TaskViewModel();
            Guid Id = id.TryParseGuidId();
            var item = await unitOfWork.TaskRepository.Get(W => W.Id == Id);
            if (item != null)
            {
                viewModel.Id = item.Id;
                viewModel.Name = item.Name;
                viewModel.ProjectId = item.ProjectId;
            }
            return viewModel;
        }

        public async Task<List<TaskViewModel>> GetAll()
        {
            List<TaskViewModel> viewModel = new List<TaskViewModel>();

            var items = await unitOfWork.TaskRepository.GetAll();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    viewModel.Add(new TaskViewModel() { Id = item.Id, Name = item.Name, ProjectId = item.ProjectId });
                }
            }
            return viewModel;
        }
    }
}
