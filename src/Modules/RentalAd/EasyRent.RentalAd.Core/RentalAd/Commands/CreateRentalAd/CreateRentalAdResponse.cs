using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.CreateRentalAd;

public record CreateRentalAdResponse(Error Error = null) : BaseResponse(Error);