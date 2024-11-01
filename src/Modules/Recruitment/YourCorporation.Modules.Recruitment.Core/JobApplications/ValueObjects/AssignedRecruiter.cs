namespace YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects
{
    internal record AssignedRecruiter
    {
        public Guid? Id { get; }

        public string FullName { get; }

        private AssignedRecruiter() { }

        private AssignedRecruiter(Guid? id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public static AssignedRecruiter Create(Guid? id, string fullName) => new(id, fullName);
    }
}
