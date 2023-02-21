using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Reservation.Core.Abstractions;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForRentalAd;

public class GetReservationsForRentalAdQueryHandler : IRequestHandler<GetReservationsForRentalAdQuery, GetReservationsForRentalAdResponse>
{
    private readonly IReservationReadOnlyRepository _reservationReadOnlyRepository;
    private readonly ILogger _logger;

    public GetReservationsForRentalAdQueryHandler(IReservationReadOnlyRepository reservationReadOnlyRepository,
        ILogger logger)
    {
        _reservationReadOnlyRepository = reservationReadOnlyRepository;
        _logger = logger;
    }
    public async Task<GetReservationsForRentalAdResponse> Handle(GetReservationsForRentalAdQuery request, CancellationToken cancellationToken)
    {
        var placeReservations = await _reservationReadOnlyRepository.GetPlaceReservationsByRentalAdId(request, cancellationToken);
        
        _logger.Info("#{PlaceReservationCount} place reservations fetched", placeReservations.TotalCount);

        return new GetReservationsForRentalAdResponse
        {
            PlaceReservations = placeReservations
        };
    }
}