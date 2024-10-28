namespace YourCorporation.Modules.Recruitment.Core.WorkLocations
{
    internal interface IWorkLocationRepository
    {
        Task<WorkLocation> GetAsync(Guid workLocationId);

        Task<IEnumerable<Guid>> GetNonExistignWorkLocationsAsync(IEnumerable<Guid> workLocationIds);

        public void Add(WorkLocation workLocation);
    }
}
