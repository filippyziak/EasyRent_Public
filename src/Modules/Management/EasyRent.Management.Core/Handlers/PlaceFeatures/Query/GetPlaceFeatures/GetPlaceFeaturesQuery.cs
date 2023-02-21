using MediatR;

namespace EasyRent.Management.Core.Handlers.PlaceFeatures.Query.GetPlaceFeatures;

public record GetPlaceFeaturesQuery : IRequest<GetPlaceFeaturesResponse>;