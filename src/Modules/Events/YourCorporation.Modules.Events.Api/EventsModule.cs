using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Modules.Events.Application;
using YourCorporation.Modules.Events.Core;
using YourCorporation.Modules.Events.Infrastructure;
using YourCorporation.Shared.Abstractions.Modules;

namespace YourCorporation.Modules.Events.Api
{
    public class EventsModule : IModule
    {
        public const string BasePath = "events-module";

        public string Name { get; } = "events";

        public string Path { get; } = BasePath;        

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCore();
            services.AddApplication();
            services.AddInfrastructure();           
        }

        public void ConfigureModule(IApplicationBuilder builder)
        {
        }
    }
}
