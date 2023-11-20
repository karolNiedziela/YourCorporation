namespace YourCorporation.Modules.Events.Core.Events.Repositories
{
    internal interface IEventRepository
    {
        Task<Event> GetAsync(Guid eventId);

        void Add(Event @event);

        void Update(Event @event);
    }
}
