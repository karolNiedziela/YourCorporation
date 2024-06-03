using MassTransit;
using YourCorporation.Modules.Events.MessagingContracts;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;

namespace YourCorporation.Modules.Forms.Api.Consumers
{
    internal class EventWentLiveConsumer : IConsumer<EventWentLive>
    {
        private readonly IEventFormRepository _eventFormRepository;
        private readonly IInboxCustomerHandler _inboxHandler;

        public EventWentLiveConsumer(IEventFormRepository eventFormRepository, IInboxCustomerHandler inboxHandler)
        {
            _eventFormRepository = eventFormRepository;
            _inboxHandler = inboxHandler;
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


            await _inboxHandler.Send(
                context, 
                typeof(EventWentLiveConsumer), 
                () => _eventFormRepository.AddAsync(eventForm));
        }
    }
}
