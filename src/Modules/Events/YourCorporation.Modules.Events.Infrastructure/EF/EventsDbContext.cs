using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Speakers;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;

namespace YourCorporation.Modules.Events.Infrastructure.EF
{
    internal class EventsDbContext : DbContext, IInboxDbSet, IOutboxDbSet
    {
        public const string SchemaName = "events";

        public DbSet<Event> Events { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<OutboxMessage> Outbox { get; set; }

        public DbSet<InboxMessage> Inbox { get; set; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
