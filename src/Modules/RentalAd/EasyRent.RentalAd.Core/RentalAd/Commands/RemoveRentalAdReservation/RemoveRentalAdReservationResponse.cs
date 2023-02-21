using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdReservation;

public record RemoveRentalAdReservationResponse(Error Error = null) : BaseResponse(Error);