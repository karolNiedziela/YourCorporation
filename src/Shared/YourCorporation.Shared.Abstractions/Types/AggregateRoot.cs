namespace YourCorporation.Shared.Abstractions.Types
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
      where TId : notnull
    {
        private readonly List<IDomainEvent> _events = new();

        public IReadOnlyList<IDomainEvent> Events => _events.ToList();

        protected AggregateRoot() : base() { }

        protected AggregateRoot(TId id) : base(id)
        {
            Id = id;
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public void ClearEvents() => _events.Clear();
    }
}
