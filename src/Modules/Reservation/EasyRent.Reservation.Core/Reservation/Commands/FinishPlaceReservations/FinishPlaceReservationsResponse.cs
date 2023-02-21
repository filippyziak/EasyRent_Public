using EasyRent.Shared.Models;

namespace EasyRent.Reservation.Core.Reservation.Commands.FinishPlaceReservations;

public record FinishPlaceReservationsResponse(Error Error = null) : BaseResponse(Error);