using System.ComponentModel.DataAnnotations.Schema;

namespace YourCorporation.Shared.Abstractions.Types
{
    public abstract class EntityClustered<TId> : IEquatable<EntityClustered<TId>>
         where TId : notnull
    {
        public TId Id { get; protected set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClusterId { get; private set;}

        protected EntityClustered() { }

        protected EntityClustered(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj) => obj is EntityClustered<TId> entity && Id.Equals(entity.Id);

        public bool Equals(EntityClustered<TId> other) => Equals((object)other);

        public static bool operator ==(EntityClustered<TId> left, EntityClustered<TId> right) => Equals(left, right);

        public static bool operator !=(EntityClustered<TId> left, EntityClustered<TId> right) => !Equals(left, right);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
