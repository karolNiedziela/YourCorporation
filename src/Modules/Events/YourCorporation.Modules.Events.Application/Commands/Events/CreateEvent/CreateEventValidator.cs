using FluentValidation;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;

namespace YourCorporation.Modules.Events.Application.Commands.Events.CreateEvent
{
    internal class CreateEventValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Category).IsInEnum();
            RuleFor(x => x.Mode).IsInEnum();
            RuleFor(x => x.AttendeesLimit)
                .Must((attendeesLimit) =>
                {
                    if (!attendeesLimit.HasValue)
                    {
                        return true;
                    }

                    return attendeesLimit.Value >= EventLimits.MinimumNumberOfAttendes;
                })
                .WithMessage($"Minimum attendees limit must be higher or equal to {EventLimits.MinimumNumberOfAttendes}");
            RuleFor(x => x.StartTime).NotNull();
            RuleFor(x => x.EndTime).NotNull().GreaterThan(addDraftEvent => addDraftEvent.StartTime);
        }
    }
}
