using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdAddress;

public record UpdateRentalAdAddressResponse(Error Error = null) : BaseResponse(Error);