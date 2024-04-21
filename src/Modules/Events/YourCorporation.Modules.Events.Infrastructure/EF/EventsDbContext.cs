using MediatR;
using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Speakers;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Infrastructure.EF
{
    internal class EventsDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public DbSet<Event> Events { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
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
               });

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
