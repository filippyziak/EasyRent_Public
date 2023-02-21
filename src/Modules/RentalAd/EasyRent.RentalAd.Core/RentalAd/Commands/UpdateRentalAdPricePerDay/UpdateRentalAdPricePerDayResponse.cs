using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPricePerDay;

public record UpdateRentalAdPricePerDayResponse(Error Error = null) : BaseResponse(Error);