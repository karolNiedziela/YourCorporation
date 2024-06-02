namespace YourCorporation.Shared.Abstractions.Messaging.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; }

        public Guid CorrelationId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string TraceId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? SentAt { get; set; }
    }
}
