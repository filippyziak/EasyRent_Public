using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Reservation.Core.Abstractions;
using EasyRent.Reservation.Domain.PlaceReservation;
using EasyRent.Reservation.Domain.PlaceReservation.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.FinishPlaceReservations;

public class FinishPlaceReservationsCommandHandler : IRequestHandler<FinishPlaceReservationsCommand, FinishPlaceReservationsResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IReservationReadOnlyRepository _reservationReadOnlyRepository;

    public FinishPlaceReservationsCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IReservationReadOnlyRepository reservationReadOnlyRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _reservationReadOnlyRepository = reservationReadOnlyRepository;
    }

    public async Task<FinishPlaceReservationsResponse> Handle(FinishPlaceReservationsCommand request, CancellationToken cancellationToken)
    {
        var reservationIdsToFinish = await _reservationReadOnlyRepository.GetPlaceReservationIdsToFinsish(cancellationToken);

        foreach (var reservationId in reservationIdsToFinish)
        {
            var placeReservation = await _aggregateRepository.LoadAsync<PlaceReservation, PlaceReservationId>(new PlaceReservationId(reservationId))
                                   ?? throw new EntityNotFoundException(reservationId, typeof(PlaceReservation));

            placeReservation.Finish();

            await _aggregateRepository.SaveAsync<PlaceReservation, PlaceReservationId>(placeReservation);

            _logger.Info("Place reservation with ID: #{PlaceReservationId} has been finished", reservationId);
        }


        return new FinishPlaceReservationsResponse();
    }
}