using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Recruitment.Core.Queues;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.Repositories;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Repositories
{
    internal class RecruitmentQueueRepository : IRecruitmentQueueRepository
    {
        private readonly DbSet<RecruitmentQueue> _recruitmentQueues;

        public RecruitmentQueueRepository(RecruitmentDbContext context)
        {
            _recruitmentQueues = context.RecruitmentQueues;
        }

        public async Task<RecruitmentQueue> GetAsync(Guid recruitmentQueueId)
          => await _recruitmentQueues.FirstOrDefaultAsync(x => x.Id == recruitmentQueueId);

        public async Task<RecruitmentQueue> GetAsync(string name)
            => await _recruitmentQueues.FirstOrDefaultAsync(x => x.Name.Value.Trim().ToLower() == name.Trim().ToLower());

        public async Task<IEnumerable<RecruitmentQueueId>> FindMatchingQueuesAsync(IEnumerable<WorkLocationId> workLocations)
        {
            var workLocationIds = workLocations.Select(wl => wl.Value).ToList();

            var matchingQueueIds = await _recruitmentQueues
                .Where(rq => rq.WorkLocations.Any(wl => workLocationIds.Contains(wl.Value)))
                .Select(rq => new RecruitmentQueueId(rq.Id))
                .ToListAsync();

            return matchingQueueIds;
        }

        public void Add(RecruitmentQueue recruitmentQueue)
            => _recruitmentQueues.Add(recruitmentQueue);
    }
}
