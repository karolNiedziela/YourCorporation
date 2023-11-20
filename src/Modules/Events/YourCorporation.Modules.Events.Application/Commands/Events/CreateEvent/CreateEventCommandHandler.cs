using MediatR;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Events.Repositories;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Modules.Events.Application.Commands.Events.CreateEvent
{
    internal class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = new Event(
               new EventId(),
               new EventName(request.Name),
               new EventDescription(request.Description),
               request.Category,
               request.Mode,
               new BegginingAndEndOfEvent(request.StartTime, request.EndTime),
               new EventLimits(request.AttendeesLimit));

            _eventRepository.Add(@event);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return @event.Id.Value;
        }
    }
}
