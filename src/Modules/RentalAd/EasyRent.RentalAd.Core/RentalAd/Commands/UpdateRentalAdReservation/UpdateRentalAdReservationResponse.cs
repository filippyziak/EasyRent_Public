using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdReservation;

public record UpdateRentalAdReservationResponse(Error Error = null) : BaseResponse(Error);