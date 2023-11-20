using MassTransit;
using YourCorporation.Modules.Events.MessagingContracts;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;

namespace YourCorporation.Modules.Forms.Api.Consumers
{
    internal class EventWentLiveConsumer : IConsumer<EventWentLive>
    {
        private readonly IEventFormRepository _eventFormRepository;

        public EventWentLiveConsumer(IEventFormRepository eventFormRepository)
        {
            _eventFormRepository = eventFormRepository;
        }

        public async Task Consume(ConsumeContext<EventWentLive> context)
        {
            var eventForm = new EventForm(Guid.NewGuid(),
                context.Message.EventId,
                context.Message.Name,
                "Description",
                context.Message.StartTime,
                context.Message.EndTime
                );

            await _eventFormRepository.AddAsync(eventForm);
        }
    }
}
