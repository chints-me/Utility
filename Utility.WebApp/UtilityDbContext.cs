using Microsoft.EntityFrameworkCore;
using Utility.WebApp.Domains;

namespace Utility.WebApp
{
    public class UtilityDbContext : DbContext
    {
        public UtilityDbContext(DbContextOptions<UtilityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UtilityDbContext).Assembly);
        }

        public DbSet<Domains.OnePercentProgress.OPP_Project> OPP_Projects { get; set; }

        public DbSet<Domains.OnePercentProgress.OPP_Task> OPP_Tasks { get; set; }
    }
}
