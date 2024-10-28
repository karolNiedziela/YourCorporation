using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using System.Reflection;
using YourCorporation.Shared.Infrastructure.Persistence;

namespace YourCorporation.Shared.Infrastructure.Messaging.Inbox
{
    internal class InboxNotificationPublisher : IInboxNotificationPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public InboxNotificationPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish<TNotification>(TNotification notification, string moduleName) where TNotification : IMessage
        {
            Type notificationType = notification.GetType();
            Type handlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);
            var handlers = _serviceProvider.GetServices(handlerType);

            // All messages are published using NotificationHandlers and changes are saved to database using UnitOfWorkNotificationHandlerDecorator, which contains _innerHandler
            // that's why is necessary to retrieve _innerHandler and on this specific innerHandler check if this is the module which should get this notification
            foreach (var handler in handlers)
            {
                var innerHandler = GetInnerHandler(handler);
                if (IsHandlerInModule(innerHandler, moduleName))
                {
                    MethodInfo handleMethod = handlerType.GetMethod("Handle");
                    await (Task)handleMethod.Invoke(handler, [notification, CancellationToken.None]);
                }
            }
        }

        private object GetInnerHandler(object handler)
        {
            var type = handler.GetType();
            var field = type.GetField("_innerHandler", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                return field.GetValue(handler);
            }
            return handler;
        }

        private bool IsHandlerInModule(object handler, string moduleName)
        {
            var handlerNamespace = handler.GetType().Namespace;
            return handlerNamespace != null && 
                   handlerNamespace.Contains($".Modules.{moduleName}.", StringComparison.OrdinalIgnoreCase);
        }
    }
}

