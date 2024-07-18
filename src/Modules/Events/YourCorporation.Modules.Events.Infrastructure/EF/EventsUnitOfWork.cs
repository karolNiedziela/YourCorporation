using YourCorporation.Shared.Abstractions.Messaging.Brokers;
using YourCorporation.Shared.Infrastructure.Persistence;

namespace YourCorporation.Modules.Events.Infrastructure.EF
{
    internal class EventsUnitOfWork : UnitOfWorkModuleContext<EventsDbContext>
    {
        public EventsUnitOfWork(EventsDbContext dbContext, IDomainEventsBroker domainEventsBroker) : base(dbContext, domainEventsBroker)
        {
        }
    }
}
