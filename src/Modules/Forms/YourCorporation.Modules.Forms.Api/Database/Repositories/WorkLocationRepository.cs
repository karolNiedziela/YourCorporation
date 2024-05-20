using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;

namespace YourCorporation.Modules.Forms.Api.Database.Repositories
{
    internal class WorkLocationRepository : IWorkLocationRepository
    {
        private readonly FormsDbContext _context;

        public WorkLocationRepository(FormsDbContext context)
        {
            _context = context;
        }

        public async Task<WorkLocation> GetAsync(Guid workLocationId)
            => await _context.WorkLocations.FirstOrDefaultAsync(x => x.Id == workLocationId);

        public async Task AddAsync(WorkLocation workLocation)
        {
            await _context.WorkLocations.AddAsync(workLocation);

            await _context.SaveChangesAsync();
        }
    }
}
