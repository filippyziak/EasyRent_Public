using EasyRent.EventSourcing;
using EasyRent.Management.Domain.PlaceFeatures.DomainEvents.V1;
using EasyRent.Management.Domain.PlaceFeatures.ValueObjects;
using EasyRent.Shared.Exceptions;

namespace EasyRent.Management.Domain.PlaceFeatures;

public class PlaceFeature : AggregateRoot<PlaceFeatureId>
{
    public PlaceFeatureId Id { get; internal set; }
    public PlaceFeatureDescription Description { get; internal set; }
    public PlaceFeaturePictureUrl PictureUrl { get; internal set; }

    public PlaceFeature(PlaceFeatureId id,
        PlaceFeatureDescription description,
        PlaceFeaturePictureUrl pictureUrl)
        => Apply(new PlaceFeatureCreated(id, description, pictureUrl));

    protected override void EnsureValidState()
    {
        var valid = Id is not null
                    && PictureUrl is not null
                    && Description is not null;

        if (!valid)
        {
            throw new InvalidEntityStateException(this, $"Place feature validation failed");
        }
    }
}