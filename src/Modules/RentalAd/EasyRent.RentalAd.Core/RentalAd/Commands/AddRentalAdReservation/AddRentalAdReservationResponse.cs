using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAdReservation;

public record AddRentalAdReservationResponse(Error Error = null) : BaseResponse(Error);