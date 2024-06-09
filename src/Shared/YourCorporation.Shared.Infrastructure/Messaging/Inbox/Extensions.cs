using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Inbox
{
    public static class Extensions
    {
        public static IServiceCollection AddInbox<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            var outboxOptions = configuration.GetSection(OutboxOptions.SectionName).Get<OutboxOptions>()!;
            if (!outboxOptions.Enabled)
            {
                return services;
            }

            services.AddTransient<IInbox, Inbox<T>>();
            services.AddTransient<Inbox<T>>();
            services.AddTransient<IInboxCustomerHandler, InboxCustomerHandler>();

            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<InboxTypeRegistry>().Register<Inbox<T>>();

            return services;
        }

        public static IServiceCollection AddInbox(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new InboxTypeRegistry());

            var outboxOptions = configuration.GetSection(OutboxOptions.SectionName).Get<OutboxOptions>()!;
            if (!outboxOptions.Enabled)
            {
                return services;
            }

            services.AddHostedService<InboxProcessor>();
            services.AddHostedService<InboxCleanupProcessor>();

            return services;
        }
    }
}
