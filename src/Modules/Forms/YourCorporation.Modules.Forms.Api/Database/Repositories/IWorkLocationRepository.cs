using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;

namespace YourCorporation.Modules.Forms.Api.Database.Repositories
{
    internal interface IWorkLocationRepository
    {
        Task<WorkLocation> GetAsync(Guid workLocationId);

        Task AddAsync(WorkLocation workLocation);
    }
}
