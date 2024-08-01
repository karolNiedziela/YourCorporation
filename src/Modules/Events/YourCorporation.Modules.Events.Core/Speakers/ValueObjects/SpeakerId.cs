namespace YourCorporation.Modules.Events.Core.Speakers.ValueObjects
{
    internal readonly record struct SpeakerId(Guid Value)
    {
        public static SpeakerId New() => new(Guid.NewGuid());

        public static implicit operator Guid(SpeakerId speakerId) => speakerId.Value;
    }
}
