using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Services;

[assembly: InternalsVisibleTo("YourCorporation.Modules.Recruitment.Application")]
[assembly: InternalsVisibleTo("YourCorporation.Modules.Recruitment.Infrastructure")]
[assembly: InternalsVisibleTo("YourCorporation.Modules.Recruitment.Api")]

namespace YourCorporation.Modules.Recruitment.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IJobApplicationService, JobApplicationService>();

            return services;
        }
    }
}
