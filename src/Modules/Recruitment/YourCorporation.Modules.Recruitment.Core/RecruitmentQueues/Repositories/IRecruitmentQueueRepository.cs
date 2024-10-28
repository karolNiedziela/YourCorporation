using YourCorporation.Modules.Recruitment.Core.Queues;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.Repositories
{
    internal interface IRecruitmentQueueRepository
    {
        Task<RecruitmentQueue> GetAsync(Guid recruitmentQueueId);

        Task<RecruitmentQueue> GetAsync(string name);

        Task<IEnumerable<RecruitmentQueueId>> FindMatchingQueuesAsync(IEnumerable<WorkLocationId> workLocations);

        void Add(RecruitmentQueue recruitmentQueue);
    }
}
