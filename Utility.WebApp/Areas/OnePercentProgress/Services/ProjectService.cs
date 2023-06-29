using Utility.WebApp.Areas.OnePercentProgress.Models;
using Utility.WebApp.Models;
using Utility.WebApp.Repositories;
using Utility.WebApp.Helpers;

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

        public async Task<ProjectViewModel> Get(string id)
        {
            ProjectViewModel viewModel = new ProjectViewModel();
            Guid Id = id.TryParseGuidId();
            var item = await unitOfWork.ProjectRepository.Get(W => W.Id == Id);
            if (item != null)
            {
                viewModel.Id = item.Id;
                viewModel.Name = item.Name;
            }
            return viewModel;
        }

        public async Task<List<ProjectViewModel>> GetAll()
        {
            List<ProjectViewModel> viewModel = new List<ProjectViewModel>();

            var items = await unitOfWork.ProjectRepository.GetAll();
            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    viewModel.Add(new ProjectViewModel() { Id = item.Id, Name = item.Name });
                }
            }
            return viewModel;
        }
    }
}
