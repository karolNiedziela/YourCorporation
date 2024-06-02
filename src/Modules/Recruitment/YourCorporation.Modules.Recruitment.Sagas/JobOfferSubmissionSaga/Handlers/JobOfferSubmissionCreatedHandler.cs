using MassTransit;
using YourCorporation.Modules.Forms.MessagingContracts;

namespace YourCorporation.Modules.Recruitment.Sagas.JobOfferSubmissionSaga.Handlers
{
    internal class JobOfferSubmissionCreatedHandler : IConsumer<JobOfferSubmissionCreated>
    {
        public Task Consume(ConsumeContext<JobOfferSubmissionCreated> context)
        {
            return Task.CompletedTask;
        }
    }
}
