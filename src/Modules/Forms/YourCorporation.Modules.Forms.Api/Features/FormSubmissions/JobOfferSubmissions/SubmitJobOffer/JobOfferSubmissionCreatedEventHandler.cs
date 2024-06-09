using MassTransit;
using MediatR;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.Events;
using YourCorporation.Modules.Forms.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Brokers;

namespace YourCorporation.Modules.Forms.Api.Features.FormSubmissions.JobOfferSubmissions.SubmitJobOffer
{
    internal class JobOfferSubmissionCreatedEventHandler : INotificationHandler<JobOfferSubmissionCreatedDomainEvent>
    {
        private readonly IMessageBroker _messageBroker;

        public JobOfferSubmissionCreatedEventHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public async Task Handle(JobOfferSubmissionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var jobOfferSubmissionCreated = new JobOfferSubmissionCreated(
                notification.JobOfferSubmissionId,
                notification.FirstName,
                notification.LastName,
                notification.Email,
                notification.CvUrl,
                notification.JobOfferId,
                notification.WorkLocationIds);

            await _messageBroker.PublishAsync(jobOfferSubmissionCreated, notification, cancellationToken);
        }
    }
}
