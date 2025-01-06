namespace YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects
{
    internal record JobApplicationId(Guid Value)
    {
        public static JobApplicationId New(Guid? jobApplicationId = null) => new(jobApplicationId ?? Guid.NewGuid());

        public static implicit operator Guid(JobApplicationId jobApplicationId) => jobApplicationId.Value;
    }
}
