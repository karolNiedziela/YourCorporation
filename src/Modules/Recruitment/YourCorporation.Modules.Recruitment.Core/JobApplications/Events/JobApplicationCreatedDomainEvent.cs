﻿using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications.Events
{
    internal record JobApplicationCreatedDomainEvent(
        Guid JobApplicationId, 
        string ApplicationFirstName, 
        string ApplicationLastName, 
        string ApplicationEmail) : IDomainEvent;
}
