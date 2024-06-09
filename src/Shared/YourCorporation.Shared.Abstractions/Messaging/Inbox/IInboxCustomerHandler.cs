using MassTransit;

namespace YourCorporation.Shared.Abstractions.Messaging.Inbox
{
    public interface IInboxCustomerHandler
    {
        Task Send(ConsumeContext<IMessage> context, Type consumerType);
    }
}
