using System.Linq;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceFeatureRemovedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceFeatureRemoved>
{
    public RentalAdPlaceFeatureRemovedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceFeatureRemoved domainEvent)
    {
        var placeFeatureToRemove = Entity.PlaceFeatures.FirstOrDefault(p
            => p.PlaceFeatureId == domainEvent.PlaceFeatureId);

        Entity.PlaceFeatures.Remove(placeFeatureToRemove);
    }
}