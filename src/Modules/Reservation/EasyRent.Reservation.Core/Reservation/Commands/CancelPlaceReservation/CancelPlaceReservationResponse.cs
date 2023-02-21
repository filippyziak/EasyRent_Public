using EasyRent.Shared.Models;

namespace EasyRent.Reservation.Core.Reservation.Commands.CancelPlaceReservation;

public record CancelPlaceReservationResponse(Error Error = null) : BaseResponse(Error);