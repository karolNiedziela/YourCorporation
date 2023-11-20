using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace YourCorporation.Shared.Infrastructure.Swagger
{
    internal static class Extensions
    {
        public static IServiceCollection AddSwaggerExtensions(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "YourCorporation API",
                    Version = "v1"
                });
            });

            services.AddEndpointsApiExplorer();

            return services;
        }
    }
}
