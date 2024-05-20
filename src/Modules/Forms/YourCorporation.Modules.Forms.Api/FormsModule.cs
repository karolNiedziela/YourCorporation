using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Modules.Forms.Api.Database;
using YourCorporation.Modules.Forms.Api.Features;
using YourCorporation.Shared.Abstractions.Modules;

namespace YourCorporation.Modules.Forms.Api
{
    public class FormsModule : IModule
    {
        public const string BasePath = "forms-module";

        public string Name => "forms";

        public string Path => BasePath;

        public void Register(IServiceCollection services, IConfiguration configratuon)
        {
            services.AddSqlServer();
            services.AddFeatures();
        }

        public void ConfigureModule(IApplicationBuilder builder)
        {
        }      
    }
}
