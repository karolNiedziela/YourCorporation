using Microsoft.EntityFrameworkCore;
using System.Linq;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Repositories
{
    internal class WorkLocationRepository : IWorkLocationRepository
    {
        private readonly DbSet<WorkLocation> _workLocations;

        public WorkLocationRepository(RecruitmentDbContext context)
        {
            _workLocations = context.WorkLocations;
        }

        public async Task<WorkLocation> GetAsync(Guid workLocationId)
          => await _workLocations.FirstOrDefaultAsync(x => x.Id == workLocationId);

        public async Task<IEnumerable<Guid>> GetNonExistignWorkLocationsAsync(IEnumerable<Guid> workLocationIds)
        {
            var ids = workLocationIds.ToList();
            var existingIds = await _workLocations
                .Where(wl => ids.Contains(wl.Id))
                .Select(wl => wl.Id.Value)
                .ToListAsync();

            return ids.Except(existingIds);
        }


        public void Add(WorkLocation workLocation)
            => _workLocations.Add(workLocation);
    }
}
