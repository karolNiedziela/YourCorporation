using Microsoft.EntityFrameworkCore;

namespace YourCorporation.Shared.Abstractions.Messaging.Outbox
{
    public interface IOutboxDbSet
    {
        public DbSet<OutboxMessage> Outbox { get; set; }
    }
}
