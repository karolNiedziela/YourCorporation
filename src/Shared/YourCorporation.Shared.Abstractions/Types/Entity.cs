namespace YourCorporation.Shared.Abstractions.Types
{
    public abstract class Entity<TId> where TId : IEquatable<TId>
    {
        public TId Id { get; protected set; }

        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id;
        }

        public static IEqualityComparer<Entity<TId>> IdEqualityComparer =>
            EqualityComparer<Entity<TId>>.Create((x, y) =>
                x is null ?
                y is null : 
                y is not null && x.GetType() == y.GetType() && x.Id.Equals(y.Id));
    }
}
