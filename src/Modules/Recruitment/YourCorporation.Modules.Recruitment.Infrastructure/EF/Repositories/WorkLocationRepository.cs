using Microsoft.EntityFrameworkCore;
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

        public void Add(WorkLocation workLocation)
            => _workLocations.Add(workLocation);
    }
}
