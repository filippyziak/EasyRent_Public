using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.DeleteRentalAd;

public record DeleteRentalAdResponse(Error Error = null) : BaseResponse(Error);