namespace YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Repositiories
{
    internal interface IJobOfferRepository
    {
        Task<JobOffer> GetAsync(Guid jobOfferId);

        Task AddAsync(JobOffer jobOffer);

        Task UpdateAsync(JobOffer jobOffer);
    }
}
