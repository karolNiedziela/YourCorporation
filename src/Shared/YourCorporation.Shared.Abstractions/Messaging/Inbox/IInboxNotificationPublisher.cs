using MediatR;

namespace YourCorporation.Shared.Abstractions.Messaging.Inbox
{
    public interface IInboxNotificationPublisher 
    {
        Task Publish<TNotification>(TNotification notification, string moduleName) where TNotification : IMessage;
    }
}
