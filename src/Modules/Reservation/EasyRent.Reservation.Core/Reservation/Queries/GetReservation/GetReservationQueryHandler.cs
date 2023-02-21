using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Reservation.Core.Abstractions;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Queries.GetReservation;

public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, GetReservationResponse>
{
    private readonly ILogger _logger;
    private readonly IReservationReadOnlyRepository _reservationReadOnlyRepository;

    public GetReservationQueryHandler(ILogger logger,
        IReservationReadOnlyRepository reservationReadOnlyRepository)
    {
        _logger = logger;
        _reservationReadOnlyRepository = reservationReadOnlyRepository;
    }
    
    public async Task<GetReservationResponse> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var placeReservation = await _reservationReadOnlyRepository.GetPlaceReservationById(request.ReservationId, cancellationToken);
        
        _logger.Info("Reservation with ID{PlaceReservationId} fetched", placeReservation.PlaceReservationId);

        return new GetReservationResponse
        {
            PlaceReservation = placeReservation
        };
    }
}