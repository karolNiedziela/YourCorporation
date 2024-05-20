using MassTransit;
using MediatR;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Events;
using YourCorporation.Modules.JobSystem.MessagingContracts;

namespace YourCorporation.Modules.JobSystem.Api.Features.JobOffers.PublishJobOffer
{
    internal sealed class JobOfferPublishedDomainEventHandler : INotificationHandler<JobOfferPublishedDomainEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public JobOfferPublishedDomainEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(JobOfferPublishedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new JobOfferPublished(
                notification.JobOffer.Id,
                notification.JobOffer.Name,
                notification.JobOffer.WorkLocations.Select(x => x.Id)), 
                cancellationToken);
        }
    }
}
