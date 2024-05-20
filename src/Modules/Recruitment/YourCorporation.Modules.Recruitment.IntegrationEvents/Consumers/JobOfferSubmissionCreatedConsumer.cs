using MassTransit;
using YourCorporation.Modules.Forms.MessagingContracts;

namespace YourCorporation.Modules.Recruitment.IntegrationEvents.Consumers
{
    internal class JobOfferSubmissionCreatedConsumer : IConsumer<JobOfferSubmissionCreated>
    {
        public Task Consume(ConsumeContext<JobOfferSubmissionCreated> context)
        {
            return Task.CompletedTask;
        }
    }
}
