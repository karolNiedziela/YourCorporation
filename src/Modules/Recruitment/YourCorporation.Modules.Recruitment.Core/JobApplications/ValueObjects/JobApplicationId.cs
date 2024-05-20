namespace YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects
{
    internal record JobApplicationId
    {
        public Guid Value { get; }

        public JobApplicationId()
        {
            Value = Guid.NewGuid();
        }

        public JobApplicationId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(JobApplicationId id) => id.Value;
    }
}
