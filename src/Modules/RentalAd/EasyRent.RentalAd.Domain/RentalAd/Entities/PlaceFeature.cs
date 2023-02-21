using System;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Entities;

public class PlaceFeature : Entity<PlaceFeatureId>
{
    public RentalAdId RentalAdId { get; internal set; }
    public PlaceFeatureId PlaceFeatureId { get; internal set; }
    public PlaceFeatureDescription Description { get; internal set; }
    public PlaceFeaturePictureUrl PictureUrl { get; internal set; }

    public PlaceFeature(Action<IDomainEvent> applier) : base(applier)
    {
    }

    public void UpdateDescription(string newDescription)
        => Apply(new RentalAdPlaceFeatureDescriptionUpdated(
            RentalAdId,
            PlaceFeatureId,
            newDescription));

    public void UpdatePictureUrl(string newPictureUrl)
        => Apply(new RentalAdPlaceFeaturePictureUrlUpdated(
            RentalAdId,
            PlaceFeatureId,
            newPictureUrl));
}