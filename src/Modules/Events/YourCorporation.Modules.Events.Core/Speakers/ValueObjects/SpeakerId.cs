using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Speakers.ValueObjects
{
    internal class SpeakerId : StronglyTypedId
    {
        public SpeakerId() : base()
        {
        }

        public SpeakerId(Guid value) : base(value)
        {
        }
    }
}
