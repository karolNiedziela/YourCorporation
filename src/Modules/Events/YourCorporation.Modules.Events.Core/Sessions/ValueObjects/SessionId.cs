namespace YourCorporation.Modules.Events.Core.Sessions.ValueObjects
{
    internal record struct SessionId
    {
        public Guid Value { get; }

        public SessionId()
        {
            Value = Guid.NewGuid();
        }

        public SessionId(Guid value)
        {
            Value = value;
        }
    }
}
