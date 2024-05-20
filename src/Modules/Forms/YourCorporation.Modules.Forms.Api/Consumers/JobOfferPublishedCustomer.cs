using MassTransit;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;
using YourCorporation.Modules.JobSystem.MessagingContracts;

namespace YourCorporation.Modules.Forms.Api.Consumers
{
    internal class JobOfferPublishedCustomer : IConsumer<JobOfferPublished>
    {
        private readonly IJobOfferFormRepository _jobOfferFormRepository;

        public JobOfferPublishedCustomer(IJobOfferFormRepository jobOfferFormRepository)
        {
            _jobOfferFormRepository = jobOfferFormRepository;
        }

        public async Task Consume(ConsumeContext<JobOfferPublished> context)
        {
            var jobOfferFormId = Guid.NewGuid();
            var jobOfferForm = new JobOfferForm(jobOfferFormId,
                context.Message.JobOfferId,
                context.Message.Name,
                context.Message.WorkLocationIds.Select(x => new JobOfferFormWorkLocation
                {
                    WorkLocationId = x,
                    JobOfferFormId = jobOfferFormId,
                }).ToList());

            await _jobOfferFormRepository.AddAsync(jobOfferForm);
        }
    }
}
