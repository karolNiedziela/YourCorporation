namespace YourCorporation.Modules.Events.Core.Speakers.ValueObjects
{
    internal record struct SpeakerId
    {
        public Guid Value { get; }

        public SpeakerId()
        {
            Value = Guid.NewGuid();
        }

        public SpeakerId(Guid value)
        {
            Value = value;
        }
    }
}
