using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdDescription;

public record UpdateRentalAdDescriptionResponse(Error Error = null) : BaseResponse(Error);