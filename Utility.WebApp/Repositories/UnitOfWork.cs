using Utility.WebApp.Repositories.OnePercentProgress;

namespace Utility.WebApp.Repositories
{
    public class UnitOfWork
    {
        private readonly UtilityDbContext dbContext;

        private ProjectRepository projectRepository;

        public UnitOfWork(UtilityDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public ProjectRepository ProjectRepository => projectRepository ??= new ProjectRepository(dbContext);

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
