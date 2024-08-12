namespace YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects
{
    internal readonly record struct JobApplicationId(Guid Value)
    {
        public static JobApplicationId New() => new(Guid.NewGuid());

        public static implicit operator Guid(JobApplicationId jobApplicationId) => jobApplicationId.Value;
    }
}
