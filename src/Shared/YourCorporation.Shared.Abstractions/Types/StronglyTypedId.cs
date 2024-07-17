namespace YourCorporation.Shared.Abstractions.Types
{
    public abstract class StronglyTypedId : IEquatable<StronglyTypedId>
    {
        public Guid Value { get; }

        protected StronglyTypedId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidOperationException("Id value cannot be empty!");
            }

            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is StronglyTypedId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(StronglyTypedId other)
        {
            return Value == other?.Value;
        }

        public static bool operator ==(StronglyTypedId obj1, StronglyTypedId obj2)
        {
            if (Equals(obj1, null))
            {
                return Equals(obj2, null);
            }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(StronglyTypedId x, StronglyTypedId y)
        {
            return !(x == y);
        }


        public static implicit operator Guid(StronglyTypedId id) => id.Value;

    }
}
