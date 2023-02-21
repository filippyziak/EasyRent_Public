using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Reservation.Core.Abstractions;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForTenant;

public class GetReservationsForTenantQueryHandler : IRequestHandler<GetReservationsForTenantQuery, GetReservationsForTenantResponse>
{
    private readonly ILogger _logger;
    private readonly IReservationReadOnlyRepository _reservationReadOnlyRepository;

    public GetReservationsForTenantQueryHandler(ILogger logger,
        IReservationReadOnlyRepository reservationReadOnlyRepository)
    {
        _logger = logger;
        _reservationReadOnlyRepository = reservationReadOnlyRepository;
    }
    public async Task<GetReservationsForTenantResponse> Handle(GetReservationsForTenantQuery request, CancellationToken cancellationToken)
    {
        var placeReservations = await _reservationReadOnlyRepository.GetPlaceReservationsByTenantId(request, cancellationToken);
        
        _logger.Info("#{PlaceReservationCount} place reservations fetched", placeReservations.TotalCount);

        return new GetReservationsForTenantResponse
        {
            PlaceReservations = placeReservations
        };
    }
}