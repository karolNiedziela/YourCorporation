using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Shared.Infrastructure.Persistence
{
    internal static class Extensions
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MSSQLOptions>(configuration.GetSection(MSSQLOptions.SectionName));

            return services;
        }
    }
}
