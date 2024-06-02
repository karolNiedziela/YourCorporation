namespace YourCorporation.Shared.Abstractions.Messaging.Brokers
{
    public interface IMessageBroker
    {
        Task PublishAsync(IMessage message, IMessage notification, CancellationToken cancellationToken = default);
    }
}
