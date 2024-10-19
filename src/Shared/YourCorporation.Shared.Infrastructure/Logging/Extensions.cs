using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace YourCorporation.Shared.Infrastructure.Logging
{
    internal static class Extensions
    {
        public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(configuration)
                 .CreateLogger();

            services.AddSerilog();

            return services;
        }

        public static IApplicationBuilder UseSerilogLogging(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            return app;
        }
    }
}
