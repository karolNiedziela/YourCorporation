using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.MediatR.Behaviors;

namespace YourCorporation.Shared.Infrastructure.MediatR
{
    internal static class Extensions
    {
        public static IServiceCollection AddBehaviors(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            return services;
        }
    }
}
