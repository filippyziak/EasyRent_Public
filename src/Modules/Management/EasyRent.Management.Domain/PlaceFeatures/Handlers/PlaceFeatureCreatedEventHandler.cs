using EasyRent.EventSourcing;
using EasyRent.Management.Domain.PlaceFeatures.DomainEvents.V1;
using EasyRent.Management.Domain.PlaceFeatures.ValueObjects;

namespace EasyRent.Management.Domain.PlaceFeatures.Handlers;

public class PlaceFeatureCreatedEventHandler : DomainEventHandler<PlaceFeature, PlaceFeatureCreated>
{
    public PlaceFeatureCreatedEventHandler(PlaceFeature entity) : base(entity)
    {
    }

    public override void Handle(PlaceFeatureCreated domainEvent)
    {
        Entity.Id = new PlaceFeatureId(domainEvent.PlaceFeatureId);
        Entity.Description = new PlaceFeatureDescription(domainEvent.Description);
        Entity.PictureUrl = new PlaceFeaturePictureUrl(domainEvent.PictureUrl);
    }
}