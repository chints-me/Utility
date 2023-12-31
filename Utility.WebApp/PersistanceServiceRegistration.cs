﻿using Microsoft.EntityFrameworkCore;
using Utility.WebApp.Areas.OnePercentProgress.Services;
using Utility.WebApp.Repositories;
using Utility.WebApp.Repositories.OnePercentProgress;

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

            // Repositories
            services.AddScoped<UnitOfWork>();
            services.AddScoped(typeof(GenericRepository<>));
            services.AddScoped<ProjectRepository>();
            services.AddScoped<TaskRepository>();

            // Services
            services.AddScoped<ProjectService>();
            services.AddScoped<TaskService>();

            return services;
        }
    }
}
