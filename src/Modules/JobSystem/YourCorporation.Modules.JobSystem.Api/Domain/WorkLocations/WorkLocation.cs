using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations
{
    internal class WorkLocation : Entity<Guid>
    {
        public string Name { get; private set; }

        public WorkLocationCode Code { get; private set; }

        public List<JobOffer> JobOffers { get; private set; } = [];

        private WorkLocation() : base() { }

        public WorkLocation(string name, WorkLocationCode code, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            Name = name;
            Code = code;
        }
    }
}
