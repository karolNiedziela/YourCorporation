namespace YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Repositories
{
    internal interface IWorkLocationRepository
    {
        Task<WorkLocation> GetAsync(Guid id);

        Task<WorkLocation> GetAsync(string code);

        Task<WorkLocation> GetAsync(Guid id, string code);

        Task AddAsync(WorkLocation location);
    }
}
