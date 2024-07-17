using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.Contacts.Events
{
    internal record ContactFromJobApplicationCreatedDomainEvent (Guid ContactId, Guid JobApplicationId) : IDomainEvent;
}
