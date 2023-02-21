using EasyRent.Reservation.ReadModels.ReadModels;
using EasyRent.Shared.Models;
using EasyRent.Shared.Pagination;

namespace EasyRent.Reservation.Core.Reservation.Queries.GetReservationsForRentalAd;

public record GetReservationsForRentalAdResponse(Error Error = null) : BaseResponse(Error)
{
    public IPagedList<PlaceReservationReadModel> PlaceReservations { get; init; } = PagedList<PlaceReservationReadModel>.Empty;
}