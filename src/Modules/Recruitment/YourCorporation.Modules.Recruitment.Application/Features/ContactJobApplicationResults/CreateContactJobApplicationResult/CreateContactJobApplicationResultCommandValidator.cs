using FluentValidation;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Constants;

namespace YourCorporation.Modules.Recruitment.Application.Features.ContactJobApplicationResults.CreateContactJobApplicationResult
{
    internal class CreateContactJobApplicationResultCommandValidator : AbstractValidator<CreateContactJobApplicationResultCommand>
    {
        public CreateContactJobApplicationResultCommandValidator()
        {
            RuleFor(x => x.JobApplicationId)
                .NotEmpty()
                .WithMessage("Job Application Id cannot be empty.");

            RuleFor(x => x.JobApplicationId)
                .NotEmpty()
                .WithMessage("Contact Id cannot be empty.");

            RuleFor(x => x.ApplicationDecision)
                .IsInEnum()
                .WithMessage("Invalid Application Decision value.");


            RuleFor(x => x.RejectedReason)
                .Must(x => x is null)
                .WithMessage("Rejected reason cannot be given when Application Decision is different to rejected.")
                .When(x => x.ApplicationDecision == ApplicationDecision.ToContact);
            
            RuleFor(x => x.RejectedReason)
                .Must(x => x is not null)
                .WithMessage("Rejected reason is required when Application Decision is rejected.")
                .When(x => x.ApplicationDecision == ApplicationDecision.Rejected);
        }
    }
}
