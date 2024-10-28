using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.Queues
{
    internal class RecruitmentQueue : AggregateRoot<RecruitmentQueueId>
    {
        public static readonly RecruitmentQueue Any = new(RecruitmentQueueName.Create("Any").Value, [], new RecruitmentQueueId(Guid.Parse("4A053A6E-6927-41C9-A3D9-3E4B74B5F1CA")));

        private readonly List<WorkLocationId> _workLocations = [];

        public RecruitmentQueueName Name { get; private set; }

        public IReadOnlyCollection<WorkLocationId> WorkLocations => _workLocations.AsReadOnly();

        private RecruitmentQueue() : base() { }

        private RecruitmentQueue(RecruitmentQueueName name, IEnumerable<WorkLocationId> workLocations, RecruitmentQueueId id = null)
            : base(id ?? RecruitmentQueueId.New())
        {
            Name = name;
            _workLocations.AddRange(workLocations);
        }

        public static RecruitmentQueue Create(RecruitmentQueueName name, IEnumerable<WorkLocationId> workLocations, RecruitmentQueueId id = null)
            => new (name, workLocations, id);
    }
}
