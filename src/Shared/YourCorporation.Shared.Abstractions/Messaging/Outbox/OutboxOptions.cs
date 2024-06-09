namespace YourCorporation.Shared.Infrastructure.Messaging.Outbox
{
    public class OutboxOptions
    {
        public const string SectionName = "Outbox";

        public bool Enabled { get; set; }

        public TimeSpan? StartDelay { get; set; }

        public TimeSpan? Interval { get; set; }

        public TimeSpan? CleanupInterval { get; set; }

        public TimeSpan? InboxCleanupInterval { get; set; }
    }
}
