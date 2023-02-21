using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.MessageBroker;
using EasyRent.Reservation.Domain.PlaceReservation;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;
using EasyRent.Reservation.Events.Reservation.Factories;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.CancelPlaceReservation;

public class CancelPlaceReservationCommandHandler : IRequestHandler<CancelPlaceReservationCommand, CancelPlaceReservationResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IMessagePublisher _messagePublisher;

    public CancelPlaceReservationCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IMessagePublisher messagePublisher)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _messagePublisher = messagePublisher;
    }

    public async Task<CancelPlaceReservationResponse> Handle(CancelPlaceReservationCommand request, CancellationToken cancellationToken)
    { 
        var placeReservation = await _aggregateRepository.LoadAsync<PlaceReservation, PlaceReservationId>(new PlaceReservationId(request.PlaceReservationId))
                               ?? throw new EntityNotFoundException(request.PlaceReservationId, typeof(PlaceReservation));

        if(request.CurrentAccountId != placeReservation.TenantId ||
           request.CurrentAccountId != placeReservation.OwnerId)
        
        placeReservation.Cancel();

        await _aggregateRepository.SaveAsync<PlaceReservation, PlaceReservationId>(placeReservation);

        await _messagePublisher.PublishMessageAsync(ReservationIntegrationEventFactory.ExchangeName,
            ReservationIntegrationEventFactory.Cancel(request.PlaceReservationId, placeReservation.RentalAdId),
            cancellationToken);

        _logger.Info("Place reservation with ID: #{PlaceReservationId} has been cancelled", request.PlaceReservationId);

        return new CancelPlaceReservationResponse();
    }
}