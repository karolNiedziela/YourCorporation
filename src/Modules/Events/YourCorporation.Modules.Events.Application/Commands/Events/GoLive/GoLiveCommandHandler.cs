using MediatR;
using YourCorporation.Modules.Events.Core;
using YourCorporation.Modules.Events.Core.Events.Repositories;
using YourCorporation.Shared.Abstractions.Exceptions;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Modules.Events.Application.Commands.Events.GoLive
{
    internal class GoLiveCommandHandler : IRequestHandler<GoLiveCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GoLiveCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(GoLiveCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetAsync(request.EventId) 
                ?? throw new CustomValidationException(ErrorCodes.Events.NotFoundEventError(request.EventId));

            @event.GoLive();

            _eventRepository.Update(@event);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
