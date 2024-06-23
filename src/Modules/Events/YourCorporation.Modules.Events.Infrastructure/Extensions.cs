using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YourCorporation.Modules.Events.Infrastructure.EF;

[assembly: InternalsVisibleTo("YourCorporation.Modules.Events.Api")]
namespace YourCorporation.Modules.Events.Infrastructure
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
