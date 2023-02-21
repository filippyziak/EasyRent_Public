using MediatR;

namespace EasyRent.Management.Core.Handlers.PlaceFeatures.Command.CreatePlaceFeature;

public record CreatePlaceFeatureCommand : IRequest<CreatePlaceFeatureResponse>
{
    public string FeatureDescription { get; init; }
    public string FeaturePictureUrl { get; init; }
}