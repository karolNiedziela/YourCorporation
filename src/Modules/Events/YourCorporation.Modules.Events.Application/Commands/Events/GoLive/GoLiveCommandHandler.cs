﻿using MediatR;
using YourCorporation.Modules.Events.Core.Events.Repositories;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Application.Commands.Events.GoLive
{
    internal class GoLiveCommandHandler : IRequestHandler<GoLiveCommand, Result>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GoLiveCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GoLiveCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetAsync(request.EventId);
            if (@event is null)
            {
                return Error.NotFound("Events.NotFoundEventError", $"Event with id '{request.EventId}' was not found.");
            }

            @event.GoLive();

            _eventRepository.Update(@event);

            await _unitOfWork.SaveChangesAsync(@event, cancellationToken);

            return Result.Success();
        }
    }
}
