using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourCorporation.Shared.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.Repositories;
using Microsoft.Extensions.Configuration;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;
using YourCorporation.Shared.Infrastructure.Messaging.Inbox;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YourCorporation.Modules.Forms.Api.Database
{
    internal static class Extensions
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FormsDbContext>((services, options) =>
            {
                var mssqlOptions = services.GetRequiredService<IOptions<MSSQLOptions>>().Value;

                options.UseSqlServer(mssqlOptions.ConnectionString, o => o
                    .MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: FormsDbContext.SchemaName));
            });

            services.AddOutbox<FormsDbContext>(configuration);
            services.AddInbox<FormsDbContext>(configuration);

            services.AddScoped<IEventFormRepository, EventFormRepository>();
            services.AddScoped<IJobOfferFormRepository, JobOfferFormRepository>();
            services.AddScoped<IWorkLocationRepository, WorkLocationRepository>();
            services.AddScoped<IJobOfferSubmissionRepository, JobOfferSubmissionRepository>();

            return services;
        }
    }
}
