using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Speakers;
using YourCorporation.Shared.Abstractions.Messaging.Brokers;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Infrastructure.EF
{
    internal class EventsDbContext : DbContext, IUnitOfWork, IInboxDbSet, IOutboxDbSet
    {
        private readonly IDomainEventsBroker _domainEventsBroker;

        public DbSet<Event> Events { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<OutboxMessage> Outbox { get; set; }

        public DbSet<InboxMessage> Inbox { get; set; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options, IDomainEventsBroker domainEventsBroker) : base(options)
        {
            _domainEventsBroker = domainEventsBroker;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("events");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker.Entries<IAggregateRoot>()
               .Select(x => x.Entity)
               .SelectMany(aggregateRoot =>
               {
                   var domainEvents = aggregateRoot.Events;
                   aggregateRoot.ClearEvents();

                   return domainEvents;
               })
               .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await _domainEventsBroker.PublishAsync(domainEvents);

            return result;
        }
    }
}
