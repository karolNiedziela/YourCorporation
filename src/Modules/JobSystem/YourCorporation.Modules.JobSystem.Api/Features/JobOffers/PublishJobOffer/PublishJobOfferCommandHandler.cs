using MediatR;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Events;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Repositiories;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Features.JobOffers.PublishJobOffer
{
    internal class PublishJobOfferCommandHandler : IRequestHandler<PublishJobOfferCommand, Result>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IPublisher _publisher;

        public PublishJobOfferCommandHandler(IJobOfferRepository jobOfferRepository, IPublisher publisher)
        {
            _jobOfferRepository = jobOfferRepository;
            _publisher = publisher;
        }

        public async Task<Result> Handle(PublishJobOfferCommand request, CancellationToken cancellationToken)
        {
            var jobOffer = await _jobOfferRepository.GetAsync(request.JobOfferId);
            if (jobOffer is null)
            {
                return Error.NotFound("JobOffers.NotFoundError", $"Job Offer with id '{request.JobOfferId} was not found.");
            }

            var result = jobOffer.Publish();

            if (result.IsFailure)
            {
                return result.Errors;
            }

            await _jobOfferRepository.UpdateAsync(jobOffer);

            await _publisher.Publish(new JobOfferPublishedDomainEvent(Guid.NewGuid(), jobOffer), cancellationToken);

            return Result.Success();
        }
    }
}
