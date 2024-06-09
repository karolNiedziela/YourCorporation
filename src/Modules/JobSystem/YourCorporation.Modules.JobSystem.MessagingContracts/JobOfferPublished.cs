﻿using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.JobSystem.MessagingContracts
{
    public record JobOfferPublished(Guid JobOfferId, string Name, IEnumerable<Guid> WorkLocationIds) : IIntegrationEvent;
}
