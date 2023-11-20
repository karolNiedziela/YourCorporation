using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Exceptions;

namespace YourCorporation.Shared.Infrastructure.Exceptions
{
    public static class Extensions
    {
        public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {
            //services.AddSingleton<ProblemDetailsFactory, BetterCRMProblemDetailsFactory>();
            services.AddScoped<GlobalExceptionHandler>();
            services.AddScoped<IExceptionToErrorResponseMapper, ExceptionToErrorResponseMapper>();

            return services;
        }

        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<GlobalExceptionHandler>();

            return applicationBuilder;
        }
    }
}
