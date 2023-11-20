using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Events.Repositories;

namespace YourCorporation.Modules.Events.Infrastructure.EF.Repositories
{
    internal class EventRepository : IEventRepository
    {
        private readonly DbSet<Event> _events;

        public EventRepository(EventsDbContext context)
        {
            _events = context.Events;
        }

        public Task<Event> GetAsync(Guid eventId)
            => _events.FirstOrDefaultAsync(x => x.Id == eventId);

        public void Add(Event @event)
            => _events.Add(@event);

        public void Update(Event @event)
            => _events.Update(@event);
    }
}
