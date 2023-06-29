using Utility.WebApp.Domains.OnePercentProgress;

namespace Utility.WebApp.Repositories.OnePercentProgress
{
    public class OPP_ProjectRepository: GenericRepository<OPP_Project>
    {
        private readonly UtilityDbContext dbContext;
        public OPP_ProjectRepository(UtilityDbContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
