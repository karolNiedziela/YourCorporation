namespace YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects
{
    internal record JobOffer
    {
        public string Name { get; }

        public string URL { get; }

        public List<string> PossibleWorkLocations { get; } = new();
    }
}
