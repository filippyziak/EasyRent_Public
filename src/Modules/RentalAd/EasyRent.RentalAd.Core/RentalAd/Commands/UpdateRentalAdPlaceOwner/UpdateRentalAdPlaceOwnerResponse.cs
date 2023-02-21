using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPlaceOwner;

public record UpdateRentalAdPlaceOwnerResponse(Error Error = null) : BaseResponse(Error);