using EasyRent.Shared.Models;

namespace EasyRent.Reservation.Core.Reservation.Commands.CreatePlaceReservation;

public record CreatePlaceReservationResponse(Error Error = null) : BaseResponse(Error);