using Microsoft.EntityFrameworkCore;

namespace Utility.WebApp.Helpers
{
    public static class MigrationHelper
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder application)
        {
            using (var scope = application.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<UtilityDbContext>())
                {
                    context.Database.Migrate();
                }
            }
            return application;
        }
    }
}
