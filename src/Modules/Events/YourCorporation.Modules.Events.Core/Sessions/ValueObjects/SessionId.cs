namespace YourCorporation.Modules.Events.Core.Sessions.ValueObjects
{
    internal readonly record struct SessionId(Guid Value)
    {
        public static SessionId New() => new(Guid.NewGuid());

        public static implicit operator Guid(SessionId sessionId) => sessionId.Value;
    }
}
