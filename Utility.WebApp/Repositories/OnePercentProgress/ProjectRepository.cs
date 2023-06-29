using Utility.WebApp.Domains.OnePercentProgress;

namespace Utility.WebApp.Repositories.OnePercentProgress
{
    public class ProjectRepository: GenericRepository<OPP_Project>
    {
        private readonly UtilityDbContext dbContext;
        public ProjectRepository(UtilityDbContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
