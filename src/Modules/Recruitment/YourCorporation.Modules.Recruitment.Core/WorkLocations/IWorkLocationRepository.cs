namespace YourCorporation.Modules.Recruitment.Core.WorkLocations
{
    internal interface IWorkLocationRepository
    {
        Task<WorkLocation> GetAsync(Guid workLocationId);

        public void Add(WorkLocation workLocation);
    }
}
