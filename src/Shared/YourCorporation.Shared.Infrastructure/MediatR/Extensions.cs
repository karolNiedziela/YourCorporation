using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.MediatR.Behaviors;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Messaging.Inbox;

namespace YourCorporation.Shared.Infrastructure.MediatR
{
    internal static class Extensions
    {
        public static IServiceCollection AddBehaviors(this IServiceCollection services)
        {
            services.AddScoped<IInboxNotificationPublisher, InboxNotificationPublisher>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

            return services;
        }
    }
}
