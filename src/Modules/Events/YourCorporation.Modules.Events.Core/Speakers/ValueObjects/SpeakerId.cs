using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Speakers.ValueObjects
{
    internal class SpeakerId(Guid Value) : StronglyTypedId(Value)
    {
    }
}
