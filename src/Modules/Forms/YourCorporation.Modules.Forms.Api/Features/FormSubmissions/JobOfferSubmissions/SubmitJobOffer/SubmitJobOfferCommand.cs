using Microsoft.AspNetCore.Http;
using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Forms.Api.Features.FormSubmissions.JobOfferSubmissions.SubmitJobOffer
{
    internal record SubmitJobOfferCommand(
        Guid JobOfferFormId,
        string FirstName,
        string LastName,
        string Email,
        IFormFile Cv,
        IEnumerable<Guid> ChosenWorkLocations) : ICommand<Result<Guid>>;
}
