using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourCorporation.Shared.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Repositiories;
using YourCorporation.Modules.JobSystem.Api.Database.Repositories;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Repositories;

namespace YourCorporation.Modules.JobSystem.Api.Database
{
    internal static class Extensions
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services)
        {
            services.AddDbContext<JobSystemDbContext>((services, options) =>
            {
                var mssqlOptions = services.GetRequiredService<IOptions<MSSQLOptions>>().Value;

                options.UseSqlServer(mssqlOptions.ConnectionString);
            });

            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<IWorkLocationRepository, WorkLocationRepository>();

            return services;
        }
    }
}
