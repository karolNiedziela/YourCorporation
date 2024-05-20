using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Modules;

namespace YourCorporation.Modules.Recruitment.Api
{
    public class RecruitmentModule : IModule
    {
        public const string BasePath = "recruitment-module";

        public string Name => "recruitment";

        public string Path => BasePath;

        public void Register(IServiceCollection services, IConfiguration configratuon)
        {
        }

        public void ConfigureModule(IApplicationBuilder builder)
        {
        }
    }
}
