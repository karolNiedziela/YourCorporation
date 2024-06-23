using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourCorporation.Modules.Events.Core.Events.Repositories;
using YourCorporation.Modules.Events.Core.Speakers.Repositories;
using YourCorporation.Modules.Events.Infrastructure.EF.Repositories;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Infrastructure.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;
using YourCorporation.Shared.Infrastructure.Persistence;

namespace YourCorporation.Modules.Events.Infrastructure.EF
{
    internal static class Extensions
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EventsDbContext>((services, options) =>
            {
                var mssqlOptions = services.GetRequiredService<IOptions<MSSQLOptions>>().Value;

                options.UseSqlServer(mssqlOptions.ConnectionString, o => o.MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: EventsDbContext.SchemaName));
            });

            services.AddUnitOfWork<EventsUnitOfWork>();

            services.AddOutbox<EventsDbContext>(configuration);
            services.AddInbox<EventsDbContext>(configuration);

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ISpeakerRepository, SpeakerRepository>();

            return services;
        }
    }
}
