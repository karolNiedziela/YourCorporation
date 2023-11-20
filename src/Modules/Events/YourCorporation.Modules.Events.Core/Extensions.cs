using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("YourCorporation.Modules.Events.Application")]
[assembly: InternalsVisibleTo("YourCorporation.Modules.Events.Infrastructure")]
[assembly: InternalsVisibleTo("YourCorporation.Modules.Events.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace YourCorporation.Modules.Events.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services;
        }
    }
}
