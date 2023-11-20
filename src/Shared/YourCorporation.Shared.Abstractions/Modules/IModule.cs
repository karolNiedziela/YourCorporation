using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace YourCorporation.Shared.Abstractions.Modules
{
    public interface IModule
    {
        string Name { get; }

        string Path { get; }

        void Register(IServiceCollection services, IConfiguration configratuon);

        void ConfigureModule(IApplicationBuilder builder);
    }
}
