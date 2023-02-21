using System;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.States;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Entities;

public class PlacePicture : Entity<PlacePictureId>
{
    public RentalAdId RentalAdId { get; internal set; }
    public PlacePictureId PlacePictureId { get; internal set; }
    public PlacePictureUrl PictureUrl { get; internal set; }
    public PlacePictureState PlacePictureState { get; internal set; }


    public PlacePicture(Action<IDomainEvent> applier) : base(applier)
    {
    }

    public void SetMainPicture()
        => Apply(new RentalAdMainPictureSet(RentalAdId, PlacePictureId));
}