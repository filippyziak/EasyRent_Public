using EasyRent.Shared.Models;

namespace EasyRent.Reservation.Core.Reservation.Commands.ReviewPlaceReservation;

public record ReviewPlaceReservationResponse(Error Error = null) : BaseResponse(Error);