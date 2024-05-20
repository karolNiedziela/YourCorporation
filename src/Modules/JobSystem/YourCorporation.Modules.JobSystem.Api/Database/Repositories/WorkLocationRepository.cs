using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Repositories;

namespace YourCorporation.Modules.JobSystem.Api.Database.Repositories
{
    internal class WorkLocationRepository : IWorkLocationRepository
    {
        private readonly JobSystemDbContext _context;

        public WorkLocationRepository(JobSystemDbContext context)
        {
            _context = context;
        }

        public async Task<WorkLocation> GetAsync(Guid id)
            => await _context.WorkLocations.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<WorkLocation> GetAsync(string code)
            => await _context.WorkLocations.FirstOrDefaultAsync(x => x.Code == code);

        public async Task<WorkLocation> GetAsync(Guid id, string code)
            => await _context.WorkLocations.FirstOrDefaultAsync(x => x.Id == id || x.Code == code);

        public async Task AddAsync(WorkLocation location)
        {
            await _context.WorkLocations.AddAsync(location);

            await _context.SaveChangesAsync();
        }
    }
}
