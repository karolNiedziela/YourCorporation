namespace YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.Repositories
{
    internal interface IJobOfferSubmissionRepository
    {
        Task AddAsync(JobOfferSubmission jobOfferSubmission);

        Task UpdateAsync(JobOfferSubmission jobOfferSubmission);
    }
}
