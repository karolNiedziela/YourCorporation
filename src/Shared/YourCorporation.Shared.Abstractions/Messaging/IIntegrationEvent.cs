using MediatR;

namespace YourCorporation.Shared.Abstractions.Messaging
{
    public interface IIntegrationEvent : INotification, IMessage
    {
    }
}
