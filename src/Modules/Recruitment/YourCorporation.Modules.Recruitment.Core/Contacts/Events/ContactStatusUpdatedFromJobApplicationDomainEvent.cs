using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.Contacts.Events
{
    internal record ContactStatusUpdatedFromJobApplicationDomainEvent(Guid JobApplicationId) : IDomainEvent;
}
