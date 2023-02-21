using System.Collections.Generic;
using EasyRent.Management.ReadModels.ReadModels;
using EasyRent.Shared.Models;

namespace EasyRent.Management.Core.Handlers.PlaceFeatures.Query.GetPlaceFeatures;

public record GetPlaceFeaturesResponse(Error Error = null) : BaseResponse(Error)
{
    public IReadOnlyList<PlaceFeatureReadModel> PlaceFeatures { get; init; }
}