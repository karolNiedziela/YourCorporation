using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourCorporation.Shared.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Repositiories;
using YourCorporation.Modules.JobSystem.Api.Database.Repositories;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Repositories;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;
using Microsoft.Extensions.Configuration;
using YourCorporation.Shared.Infrastructure.Messaging.Inbox;

namespace YourCorporation.Modules.JobSystem.Api.Database
{
    internal static class Extensions
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JobSystemDbContext>((services, options) =>
            {
                var mssqlOptions = services.GetRequiredService<IOptions<MSSQLOptions>>().Value;

                options.UseSqlServer(mssqlOptions.ConnectionString);
            });

            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<IWorkLocationRepository, WorkLocationRepository>();

            services.AddOutbox<JobSystemDbContext>(configuration);
            services.AddInbox<JobSystemDbContext>(configuration);

            return services;
        }
    }
}
