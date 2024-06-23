using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Modules.Recruitment.Application;
using YourCorporation.Modules.Recruitment.Infrastructure;
using YourCorporation.Shared.Abstractions.Modules;
using YourCorporation.Modules.Recruitment.Core;

namespace YourCorporation.Modules.Recruitment.Api
{
    public class RecruitmentModule : IModule
    {
        public const string BasePath = "recruitment-module";

        public string Name => "recruitment";

        public string Path => BasePath;

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCore();
            services.AddApplication();
            services.AddInfrastructure(configuration);
        }

        public void ConfigureModule(IApplicationBuilder builder)
        {
        }
    }
}
