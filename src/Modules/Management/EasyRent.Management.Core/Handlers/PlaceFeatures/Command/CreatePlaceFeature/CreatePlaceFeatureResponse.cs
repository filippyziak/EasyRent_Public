using EasyRent.Shared.Models;

namespace EasyRent.Management.Core.Handlers.PlaceFeatures.Command.CreatePlaceFeature;

public record CreatePlaceFeatureResponse(Error Error = null) : BaseResponse(Error);