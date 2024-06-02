using MediatR;
using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Shared.Abstractions.Types
{
    public interface IDomainEvent : INotification, IMessage
    {
    }
}
