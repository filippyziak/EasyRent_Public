using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.RemoveRentalAdPlaceFeatures;

public record RemoveRentalAdPlaceFeaturesResponse(Error Error = null) : BaseResponse(Error);