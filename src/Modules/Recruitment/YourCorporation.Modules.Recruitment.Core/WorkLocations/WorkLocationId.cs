using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.WorkLocations
{
    internal class WorkLocationId(Guid Value) : StronglyTypedId(Value)
    {
    }
}
