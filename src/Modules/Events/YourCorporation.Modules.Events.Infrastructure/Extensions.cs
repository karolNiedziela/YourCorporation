using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YourCorporation.Modules.Events.Infrastructure.EF;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;

[assembly: InternalsVisibleTo("YourCorporation.Modules.Events.Api")]
namespace YourCorporation.Modules.Events.Infrastructure
{   
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer();

            services.AddOutbox<EventsDbContext>(configuration);

            return services;
        }
    }
}
