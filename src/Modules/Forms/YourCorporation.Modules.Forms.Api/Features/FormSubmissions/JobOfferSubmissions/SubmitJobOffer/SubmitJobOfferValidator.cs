using FluentValidation;
using YourCorporation.Shared.Abstractions.Validators.FluentValidators;

namespace YourCorporation.Modules.Forms.Api.Features.FormSubmissions.JobOfferSubmissions.SubmitJobOffer
{
    internal class SubmitJobOfferValidator : AbstractValidator<SubmitJobOfferCommand>
    {
        public SubmitJobOfferValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.JobOfferFormId).NotNull().NotEmpty();
            RuleFor(x => x.Cv).SetValidator(new PDFValidator());
        }
    }
}
