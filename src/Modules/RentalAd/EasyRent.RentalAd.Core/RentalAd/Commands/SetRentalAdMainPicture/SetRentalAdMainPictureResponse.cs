using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.SetRentalAdMainPicture;

public record SetRentalAdMainPictureResponse(Error Error = null) : BaseResponse(Error);