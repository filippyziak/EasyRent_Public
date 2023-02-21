using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.AddRentalAdPlaceFeatures;

public record AddRentalAdPlaceFeaturesResponse(Error Error = null) : BaseResponse(Error);