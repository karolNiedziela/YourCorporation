namespace YourCorporation.Shared.Abstractions.Types
{
    public abstract class EntityStronglyTyped<TId> : IEquatable<EntityStronglyTyped<TId>>
        where TId : StronglyTypedId
    {
        public TId Id { get; protected set;}

        protected EntityStronglyTyped() { }

        protected EntityStronglyTyped(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj) => obj is EntityStronglyTyped<TId> entity && Id.Equals(entity.Id);

        public bool Equals(EntityStronglyTyped<TId> other) => Equals((object)other);

        public static bool operator ==(EntityStronglyTyped<TId> left, EntityStronglyTyped<TId> right) => Equals(left, right);

        public static bool operator !=(EntityStronglyTyped<TId> left, EntityStronglyTyped<TId> right) => !Equals(left, right);

        public override int GetHashCode() =>  Id.GetHashCode();       
    }
}
