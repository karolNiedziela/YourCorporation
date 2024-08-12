using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.WorkLocations
{
    internal class WorkLocation : AggregateRoot<WorkLocationId>
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        private WorkLocation() { }

        public WorkLocation(WorkLocationId id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }
    }
}
