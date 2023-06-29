using Utility.WebApp.Repositories.OnePercentProgress;

namespace Utility.WebApp.Repositories
{
    public class UnitOfWork
    {
        private readonly UtilityDbContext dbContext;

        private OPP_ProjectRepository opp_ProjectRepository;

        public UnitOfWork(UtilityDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public OPP_ProjectRepository OPP_ProjectRepository => opp_ProjectRepository ??= new OPP_ProjectRepository(dbContext);

        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
