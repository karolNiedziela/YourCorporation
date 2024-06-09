using MassTransit;

namespace YourCorporation.Shared.Abstractions.Messaging.Inbox
{
    public interface IInbox
    {
        public bool Enabled { get; }

        Task SaveAsync(ConsumeContext<IMessage> consumeContext);

        Task ProcessUnprocessedAsync();

        Task CleanupAsync(DateTime? to = null);
    }
}
