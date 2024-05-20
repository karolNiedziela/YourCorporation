using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Features.JobOffers.PublishJobOffer
{
    internal record PublishJobOfferCommand(Guid JobOfferId) : ICommand<Result>;
}
