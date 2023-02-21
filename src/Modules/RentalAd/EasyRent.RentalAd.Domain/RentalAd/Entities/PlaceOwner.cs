using System;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Entities;

public class PlaceOwner : Entity<PlaceOwnerId>
{
    public RentalAdId RentalAdId { get; internal set; }
    public PlaceOwnerId PlaceOwnerId { get; internal set; }
    public PlaceOwnerEmailAddress EmailAddress { get; internal set; }
    public PlaceOwnerPictureUrl PictureUrl { get; internal set; }

    public PlaceOwner(Action<IDomainEvent> applier) : base(applier)
    {
    }

    public void UpdateEmailAddress(PlaceOwnerEmailAddress newEmailAddress)
        => Apply(new RentalAdPlaceOwnerEmailAddressUpdated(
            RentalAdId,
            PlaceOwnerId,
            newEmailAddress));

    public void UpdatePictureUrl(PlaceOwnerPictureUrl newPictureUrl)
        => Apply(new RentalAdPlaceOwnerPictureUrlUpdated(
            RentalAdId,
            PlaceOwnerId,
            newPictureUrl));

    public void Delete()
        => Apply(new RentalAdPlaceOwnerDeleted(
            RentalAdId,
            PlaceOwnerId));
}