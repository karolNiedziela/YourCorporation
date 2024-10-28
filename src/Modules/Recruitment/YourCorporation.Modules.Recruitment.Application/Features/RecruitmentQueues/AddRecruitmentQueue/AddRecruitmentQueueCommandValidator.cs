using FluentValidation;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Application.Features.RecruitmentQueues.AddRecruitmentQueue
{
    internal class AddRecruitmentQueueCommandValidator : AbstractValidator<AddRecruitmentQueueCommand>
    {
        public AddRecruitmentQueueCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty.")
                .MaximumLength(RecruitmentQueueName.MaxLength)
                .WithMessage($"Name cannot be longer than {RecruitmentQueueName.MaxLength} characters.")
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Name cannot consist only of white spaces.");

            RuleFor(x => x.WorkLocationIds)
                .NotNull()
                .WithMessage("Work locations cannot be null")
                .Must(x => x.Count != 0)
                .WithMessage("At least one work location must be specified.");

            RuleFor(x => x.WorkLocationIds)
                .Must(x => x.Distinct().Count() == x.Count)
                .When(x => x.WorkLocationIds != null)
                .WithMessage("Duplicate work location IDs are not allowed.");
        }
    }
}
