using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Auth;

namespace YourCorporation.Modules.Users.Api.Services
{
    internal static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPermissionService, PermissionService>();

            return services;
        }
    }
}
