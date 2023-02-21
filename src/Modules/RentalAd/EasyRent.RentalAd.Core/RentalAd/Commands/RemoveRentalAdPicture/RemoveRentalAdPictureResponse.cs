using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdPicture;

public record RemoveRentalAdPictureResponse(Error Error = null) : BaseResponse(Error);
