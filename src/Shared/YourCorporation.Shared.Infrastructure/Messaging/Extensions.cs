using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration, params Assembly[] scanAssemblies)
        {
            services.AddRabbitMQ(configuration);

            services.ConfigureMassTransit(scanAssemblies);

            return services;
        }

        private static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQOptions>(configuration.GetSection(RabbitMQOptions.SectionName));

            return services;
        }

        private static IServiceCollection ConfigureMassTransit(this IServiceCollection services, params Assembly[] scanAssemblies)
        {
            var assemblies = scanAssemblies.Any() ? scanAssemblies : AppDomain.CurrentDomain.GetAssemblies();

            var configurations = assemblies.SelectMany(x => x.GetTypes()).Where(t =>
               t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsInterface
               && t.GetConstructor(Type.EmptyTypes) != null
               && typeof(IMassTransitDefinition).IsAssignableFrom(t))
                .ToList();

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                foreach (var configuration in configurations)
                {
                    var instantiatedType = (IMassTransitDefinition)Activator.CreateInstance(configuration)!;
                    instantiatedType.RegisterConsumers(x);
                }

                x.UsingRabbitMq((context, config) =>
                {
                    config.PrefetchCount = 50;

                    var rabbitMQOptions = context.GetRequiredService<IOptions<RabbitMQOptions>>().Value;

                    config.Host(rabbitMQOptions.HostName, rabbitMQOptions.VirtualHost, h =>
                    {
                        h.Username(rabbitMQOptions.Username);
                        h.Password(rabbitMQOptions.Password);
                    });

                    foreach (var configuration in configurations)
                    {
                        var instantiatedType = (IMassTransitDefinition)Activator.CreateInstance(configuration)!;
                        instantiatedType.ConfigureMassTransit(context, config);
                    }

                    config.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
