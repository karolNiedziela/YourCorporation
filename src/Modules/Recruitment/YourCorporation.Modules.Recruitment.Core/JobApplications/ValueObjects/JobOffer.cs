namespace YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects
{
    internal record JobOffer
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        private JobOffer() { }

        public JobOffer(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
