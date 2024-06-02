namespace YourCorporation.Shared.Abstractions.Messaging.Outbox
{
    public interface IOutbox
    {
        bool Enabled { get; }

        Task SaveAsync(params IMessage[] messages);

        Task PublishUnsentAsync();

        Task CleanupAsync(DateTime? to = null);
    }
}
