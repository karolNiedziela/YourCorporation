using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.WorkLocations
{
    internal class WorkLocationId : StronglyTypedId
    {
        public WorkLocationId() : base()
        {
        }

        public WorkLocationId(Guid value) : base(value)
        {
        }
    }
}
