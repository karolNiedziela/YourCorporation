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
                JobOfferSubmissionId: notification.JobOfferSubmissionId,
                FirstName: notification.FirstName,
                LastName: notification.LastName,
                CVUrl: notification.CvUrl,
                Email: notification.Email,
                JobOfferId: notification.JobOfferId,
                ChosenWorkLocationIds: notification.WorkLocationIds);

            await _messageBroker.PublishAsync(jobOfferSubmissionCreated, notification, cancellationToken);
        }
    }
}
