namespace YourCorporation.Shared.Abstractions.Messaging.Brokers
{
    public interface IDomainEventsBroker
    {
        Task PublishAsync(IMessage message, CancellationToken cancellationToken = default);

        Task PublishAsync(IMessage[] messages, CancellationToken cancellationToken = default);

        Task PublishAsync(IMessage sourceNotification, IMessage message, CancellationToken cancellationToken = default);

        Task PublishAsync(IMessage sourceNotification, IMessage[] messages, CancellationToken cancellationToken = default);
    }
}
