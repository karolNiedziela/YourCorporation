using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications.Events
{
    internal record JobApplicationCreatedDomainEvent(Guid JobApplicationId, FirstName ApplicationFirstName, LastName ApplicationLastName, PrivateEmail ApplicationEmail) : IDomainEvent;
}
