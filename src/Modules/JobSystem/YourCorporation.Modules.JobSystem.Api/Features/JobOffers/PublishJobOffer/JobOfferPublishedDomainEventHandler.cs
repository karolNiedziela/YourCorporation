using MassTransit;
using MediatR;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Events;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Brokers;

namespace YourCorporation.Modules.JobSystem.Api.Features.JobOffers.PublishJobOffer
{
    internal sealed class JobOfferPublishedDomainEventHandler : INotificationHandler<JobOfferPublishedDomainEvent>
    {
        private readonly IMessageBroker _messageBroker;

        public JobOfferPublishedDomainEventHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public async Task Handle(JobOfferPublishedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _messageBroker.PublishAsync(new JobOfferPublished(
                notification.JobOffer.Id,
                notification.JobOffer.Name,
                notification.JobOffer.WorkLocations.Select(x => x.Id)),
                notification,
                cancellationToken);
        }
    }
}
