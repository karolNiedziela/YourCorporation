using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourCorporation.Modules.Recruitment.Core.Candidates.Repositories;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;
using YourCorporation.Modules.Recruitment.Infrastructure.EF.Repositories;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Infrastructure.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;
using YourCorporation.Shared.Infrastructure.Persistence;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF
{
    internal static class Extensions
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RecruitmentDbContext>((services, options) =>
            {
                var mssqlOptions = services.GetRequiredService<IOptions<MSSQLOptions>>().Value;

                options.UseSqlServer(mssqlOptions.ConnectionString, o =>
                    o.MigrationsHistoryTable(
                        tableName: HistoryRepository.DefaultTableName,
                        schema: RecruitmentDbContext.SchemaName));
            });

            services.AddUnitOfWork<RecruitmentUnitOfWork>();

            services.AddOutbox<RecruitmentDbContext>(configuration);
            services.AddInbox<RecruitmentDbContext>(configuration);

            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<IWorkLocationRepository, WorkLocationRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();         

            return services;
        }
    }
}
