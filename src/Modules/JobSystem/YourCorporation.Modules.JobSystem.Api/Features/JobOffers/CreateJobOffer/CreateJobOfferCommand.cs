using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Features.JobOffers.CreateJobOffer
{
    internal record CreateJobOfferCommand(string Name, IEnumerable<Guid> WorkLocations) : ICommand<Result<Guid>>;
}
