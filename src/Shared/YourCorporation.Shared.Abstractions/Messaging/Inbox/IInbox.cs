namespace YourCorporation.Shared.Abstractions.Messaging.Inbox
{
    public interface IInbox
    {
        public bool Enabled { get; }

        Task HandleAsync(Guid messageId, string name, Func<Task> handler);
    }
}
