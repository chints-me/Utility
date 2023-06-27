using Microsoft.EntityFrameworkCore;

namespace Utility.WebApp
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistanceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UtilityDbContext>
                (options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("AppConnection"));
                    options.EnableSensitiveDataLogging();
                });

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IApplicationRepository, ApplicationRepository>();

            return services;
        }
    }
}
