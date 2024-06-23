using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YourCorporation.Modules.Recruitment.Infrastructure.EF;
using YourCorporation.Shared.Infrastructure.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;

[assembly: InternalsVisibleTo("YourCorporation.Modules.Recruitment.Api")]
namespace YourCorporation.Modules.Recruitment.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration);           

            return services;
        }
    }
}
