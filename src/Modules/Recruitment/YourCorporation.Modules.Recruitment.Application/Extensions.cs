using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Infrastructure.Persistence;

[assembly: InternalsVisibleTo("YourCorporation.Modules.Recruitment.Infrastructure")]
[assembly: InternalsVisibleTo("YourCorporation.Modules.Recruitment.Api")]
namespace YourCorporation.Modules.Recruitment.Application
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

            DecorateAllNotificationHandlers(applicationAssembly, services);

            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
            });

            services.AddValidatorsFromAssembly(applicationAssembly, includeInternalTypes: true);

            return services;
        }

        private static void DecorateAllNotificationHandlers(Assembly assembly, IServiceCollection services)
        {
            var notificationHandlerType = typeof(INotificationHandler<>);
            var descriptorsToDecorate = services
                .Where(d => d.ServiceType.IsGenericType &&
                            d.ServiceType.GetGenericTypeDefinition() == notificationHandlerType &&
                            d.ImplementationType != null &&
                            d.ImplementationType.Assembly == assembly)
                .ToList();

            foreach (var descriptor in descriptorsToDecorate)
            {
                var genericArguments = descriptor.ServiceType.GenericTypeArguments;
                if (genericArguments.Length > 0)
                {
                    var notificationType = genericArguments[0];
                    var decoratorType = typeof(UnitOfWorkNotificationHandlerDecorator<>).MakeGenericType(notificationType);

                    services.Decorate(descriptor.ServiceType, decoratorType);
                }
            }
        }
    }
}
