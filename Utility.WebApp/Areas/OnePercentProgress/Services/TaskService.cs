using Utility.WebApp.Areas.OnePercentProgress.Models;
using Utility.WebApp.Enums;
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
            item.OPP_ProjectId = viewModel.ProjectId;
            item.OPP_ParentTaskId = viewModel.ParentTaskId;
            item.Status = viewModel.Status;
            item = await unitOfWork.TaskRepository.Add(item);

            if (item.Status != TaskStatusEnum.Done.ToValue())
            {
                await MakeParentTaskInProgress(item.OPP_ParentTaskId);
            }

            await unitOfWork.Save();

            response.IsSuccess = true;
            response.Data = item.Id.ToString();
            return response;
        }

        public async Task<ResponseViewModel> Update(TaskViewModel viewModel)
        {
            ResponseViewModel response = new ResponseViewModel();

            var item = await unitOfWork.TaskRepository.Get(W => W.Id == viewModel.Id);
            item.Name = viewModel.Name;
            item.OPP_ProjectId = viewModel.ProjectId;
            item.OPP_ParentTaskId = viewModel.ParentTaskId;
            item.Status = viewModel.Status;
            await unitOfWork.TaskRepository.Update(item);

            if (item.Status != TaskStatusEnum.Done.ToValue())
            {
                await MakeParentTaskInProgress(item.OPP_ParentTaskId);
            }

            await unitOfWork.Save();

            response.IsSuccess = true;
            response.Data = item.Id.ToString();
            return response;
        }

        private async Task MakeParentTaskInProgress(Guid parentTaskId)
        {
            var parentTask = await unitOfWork.TaskRepository.Get(W => W.Id == parentTaskId);
            if (parentTask != null)
            {
                parentTask.Status = TaskStatusEnum.InProgress.ToValue();
                await unitOfWork.TaskRepository.Update(parentTask);

                await MakeParentTaskInProgress(parentTask.OPP_ParentTaskId);
            }
        }

        private void GetTaskCompletionStats(Guid taskId, out int totalTasks, out int totalTasksDone, out int completionPercentage)
        {
            totalTasks = 0;
            totalTasksDone = 0;
            completionPercentage = 0;
            var task = unitOfWork.TaskRepository.Get(W => W.Id == taskId).Result;
            if (task != null)
            {
                var tasks = unitOfWork.TaskRepository.GetAll(W => W.OPP_ParentTaskId == taskId).Result;
                if (tasks.Any())
                {
                    totalTasks = tasks.Count();
                    totalTasksDone = tasks.Where(W => W.Status == TaskStatusEnum.Done.ToValue()).Count();
                }
                else
                {

                    totalTasks = 1;
                    totalTasksDone = task.Status == TaskStatusEnum.Done.ToValue() ? 1 : 0;
                }
                completionPercentage = totalTasks > 0 ? (int)(((double)totalTasksDone / totalTasks) * 100) : 0;
            }
        }

        public async Task<TaskViewModel> Get(Guid id, Guid parentTaskId, Guid projectId)
        {
            TaskViewModel viewModel = new TaskViewModel();

            viewModel.StatusList = TaskStatusEnumExtensions.GetTaskStatus();
            viewModel.Status = TaskStatusEnum.Pending.ToValue();
            viewModel.ProjectId = projectId;
            viewModel.ParentTaskId = parentTaskId;

            var item = await unitOfWork.TaskRepository.Get(W => W.Id == id && W.OPP_ParentTaskId == parentTaskId && W.OPP_ProjectId == projectId);
            if (item != null)
            {
                viewModel.Id = item.Id;
                viewModel.Name = item.Name;
                viewModel.ProjectId = item.OPP_ProjectId;
                viewModel.ParentTaskId = item.OPP_ParentTaskId;
                viewModel.Status = item.Status;

                GetTaskCompletionStats(item.Id, out int totalTasks, out int totalTasksDone, out int completionPercentage);
                viewModel.TotalTasks = totalTasks;
                viewModel.TotalTasksDone = totalTasksDone;
                viewModel.CompletionPercentage = completionPercentage;

            }
            return viewModel;
        }

        public async Task<TaskViewModel?> Get(Guid id)
        {
            TaskViewModel? viewModel = null;
            var item = await unitOfWork.TaskRepository.Get(W => W.Id == id);
            if (item != null)
            {
                viewModel = new TaskViewModel();
                viewModel.StatusList = TaskStatusEnumExtensions.GetTaskStatus();

                viewModel.Id = item.Id;
                viewModel.Name = item.Name;
                viewModel.ProjectId = item.OPP_ProjectId;
                viewModel.ParentTaskId = item.OPP_ParentTaskId;
                viewModel.Status = item.Status;

                GetTaskCompletionStats(item.Id, out int totalTasks, out int totalTasksDone, out int completionPercentage);
                viewModel.TotalTasks = totalTasks;
                viewModel.TotalTasksDone = totalTasksDone;
                viewModel.CompletionPercentage = completionPercentage;
            }
            return viewModel;
        }

        public async Task<List<TaskViewModel>> GetAll(Guid parentTaskId, Guid projectId)
        {
            List<TaskViewModel> viewModel = new List<TaskViewModel>();

            var items = await unitOfWork.TaskRepository.GetAll();

            if (parentTaskId != Guid.Empty && projectId != Guid.Empty)
            {
                items = items.Where(W => W.OPP_ParentTaskId == parentTaskId && W.OPP_ProjectId == projectId).ToList();
            }
            else if (parentTaskId == Guid.Empty && projectId != Guid.Empty)
            {
                items = items.Where(W => W.OPP_ProjectId == projectId && W.OPP_ParentTaskId == Guid.Empty).ToList();
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    GetTaskCompletionStats(item.Id, out int totalTasks, out int totalTasksDone, out int completionPercentage);
                    viewModel.Add(new TaskViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ProjectId = item.OPP_ProjectId,
                        ParentTaskId = item.OPP_ParentTaskId,
                        Status = item.Status,
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
