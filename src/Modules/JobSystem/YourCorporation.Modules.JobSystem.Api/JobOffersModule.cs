using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Modules;
using YourCorporation.Modules.JobSystem.Api.Database;
using YourCorporation.Modules.JobSystem.Api.Features;

namespace YourCorporation.Modules.JobSystem.Api
{
    public class JobOffersModule : IModule
    {
        public const string BasePath = "jobsystem-module";

        public string Name => "jobsystem";

        public string Path => BasePath;
        
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration);
            services.AddFeatures();
        }

        public void ConfigureModule(IApplicationBuilder builder)
        {
        }
    }
}
