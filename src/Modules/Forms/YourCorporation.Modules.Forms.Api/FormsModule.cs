using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YourCorporation.Modules.Forms.Api.Database;
using YourCorporation.Shared.Abstractions.Messaging;
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
        }

        public void ConfigureModule(IApplicationBuilder builder)
        {
        }      
    }
}
