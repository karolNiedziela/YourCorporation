using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace YourCorporation.Modules.Users.Api.Features
{
    internal static class Extensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services)
        {
            var applicationAssembly = typeof(UsersModule).Assembly;

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
