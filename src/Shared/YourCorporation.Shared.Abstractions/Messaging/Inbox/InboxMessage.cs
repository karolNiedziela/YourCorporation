namespace YourCorporation.Shared.Abstractions.Messaging.Inbox
{
    public class InboxMessage
    {
        public Guid Id { get; set; }

        public Guid CorrelationId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime ReceivedAt { get; set; }

        public DateTime? ProcessedAt { get; set; }
    }
}
