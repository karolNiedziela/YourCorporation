namespace YourCorporation.Shared.Abstractions.Messaging.Outbox
{
    public interface IOutboxBroker
    {
        Task SendAsync(params IMessage[] messages);
    }
}
