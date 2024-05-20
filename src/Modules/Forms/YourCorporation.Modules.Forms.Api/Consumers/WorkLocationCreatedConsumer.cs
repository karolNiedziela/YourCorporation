using MassTransit;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;
using YourCorporation.Modules.JobSystem.MessagingContracts;

namespace YourCorporation.Modules.Forms.Api.Consumers
{
    internal class WorkLocationCreatedConsumer : IConsumer<WorkLocationCreated>
    {
        private readonly IWorkLocationRepository _workLocationRepository;

        public WorkLocationCreatedConsumer(IWorkLocationRepository workLocationRepository)
        {
            _workLocationRepository = workLocationRepository;
        }

        public async Task Consume(ConsumeContext<WorkLocationCreated> context)
        {
            var workLocation = new WorkLocation
            {
                Id = context.Message.Id,
                Name = context.Message.Name
            };

            await _workLocationRepository.AddAsync(workLocation);
        }
    }
}
