using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.WorkLocations
{
    internal class WorkLocationId
    {
        public Guid Value { get; }

        public WorkLocationId()
        {
            Value = Guid.NewGuid();
        }

        public WorkLocationId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(WorkLocationId id) => id.Value;
    }
}
