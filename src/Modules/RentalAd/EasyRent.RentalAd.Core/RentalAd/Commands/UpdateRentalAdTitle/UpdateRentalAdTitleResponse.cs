using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdTitle;

public record UpdateRentalAdTitleResponse(Error Error = null) : BaseResponse(Error);