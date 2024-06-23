using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using YourCorporation.Shared.Abstractions.MediatR.Behaviors;
using YourCorporation.Shared.Infrastructure.Persistence;
using MediatR.Pipeline;

[assembly: InternalsVisibleTo("YourCorporation.Modules.Events.Infrastructure")]
[assembly: InternalsVisibleTo("YourCorporation.Modules.Events.Api")]
namespace YourCorporation.Modules.Events.Application
{
    internal static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ApplicationAssemblyReference).Assembly;

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(applicationAssembly);
            });

            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
            });

            services.AddValidatorsFromAssembly(applicationAssembly, includeInternalTypes: true);

            return services;
        }
    }
}
