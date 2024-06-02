using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Outbox
{
    public static class Extensions
    {
        public static IServiceCollection AddOutbox<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {          
            var outboxOptions = configuration.GetSection(OutboxOptions.SectionName).Get<OutboxOptions>()!;
            if (!outboxOptions.Enabled)
            {
                return services;
            }

            services.AddTransient<IOutbox, Outbox<T>>();
            services.AddTransient<Outbox<T>>();

            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<OutboxTypeRegistry>().Register<Outbox<T>>();

            return services;
        }

        public static IServiceCollection AddOutbox(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OutboxOptions>(configuration.GetSection(OutboxOptions.SectionName));

            var outboxOptions = configuration.GetSection(OutboxOptions.SectionName).Get<OutboxOptions>()!;
            services.AddSingleton<IOutboxBroker, OutboxBroker>();
            services.AddSingleton(new OutboxTypeRegistry());

            if (!outboxOptions.Enabled)
            {
                return services;
            }

            services.AddHostedService<OutboxProcessor>();
            services.AddHostedService<OutboxCleanupProcessor>();

            return services;
        }
    }
}
