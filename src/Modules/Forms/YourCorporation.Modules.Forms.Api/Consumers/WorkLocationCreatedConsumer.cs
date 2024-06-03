using MassTransit;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Modules.Forms.Api.Consumers
{
    internal class WorkLocationCreatedConsumer : IConsumer<WorkLocationCreated>
    {
        private readonly IWorkLocationRepository _workLocationRepository;
        private readonly IInboxCustomerHandler _inboxHandler;

        public WorkLocationCreatedConsumer(IWorkLocationRepository workLocationRepository, IInboxCustomerHandler inboxHandler)
        {
            _workLocationRepository = workLocationRepository;
            _inboxHandler = inboxHandler;
        }

        public async Task Consume(ConsumeContext<WorkLocationCreated> context)
        {
            var workLocation = new WorkLocation
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                Code = context.Message.Code,
            };

            await _inboxHandler.Send(
                context,
                typeof(WorkLocationCreatedConsumer),
                () => _workLocationRepository.AddAsync(workLocation));
        }
    }
}
