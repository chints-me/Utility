using Utility.WebApp.Domains.OnePercentProgress;

namespace Utility.WebApp.Repositories.OnePercentProgress
{
    public class TaskRepository : GenericRepository<OPP_Task>
    {
        private readonly UtilityDbContext dbContext;

        public TaskRepository(UtilityDbContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
