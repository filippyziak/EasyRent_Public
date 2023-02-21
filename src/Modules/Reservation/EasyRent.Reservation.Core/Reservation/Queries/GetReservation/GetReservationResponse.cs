using EasyRent.Reservation.ReadModels.ReadModels;
using EasyRent.Shared.Models;

namespace EasyRent.Reservation.Core.Reservation.Queries.GetReservation;

public record GetReservationResponse(Error Error = null) : BaseResponse(Error)
{
    public PlaceReservationReadModel PlaceReservation { get; init; }
}