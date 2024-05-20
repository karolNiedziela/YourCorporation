using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;

namespace YourCorporation.Modules.Forms.Api.Database.Repositories
{
    internal interface IJobOfferFormRepository
    {
        Task<JobOfferForm> GetAsync(Guid id);

        Task AddAsync(JobOfferForm form);

        Task UpdateAsync(JobOfferForm form);
    }
}
