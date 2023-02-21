using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdPictureAddedEventHandler : DomainEventHandler<RentalAd, RentalAdPictureAdded>
{
    public RentalAdPictureAddedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPictureAdded domainEvent)
    {
        var placePicture = new PlacePicture(Entity.Apply);
        Entity.ApplyToEntity(placePicture, domainEvent);

        Entity.PlacePictures.Add(placePicture);
    }
}

public sealed class PlacePictureRentalAdPictureAddedEventHandler : DomainEventHandler<PlacePicture, RentalAdPictureAdded>
{
    public PlacePictureRentalAdPictureAddedEventHandler(PlacePicture entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPictureAdded domainEvent)
    {
        Entity.RentalAdId = new RentalAdId(domainEvent.RentalAdId);
        Entity.PlacePictureId = new PlacePictureId(domainEvent.PlacePictureId);
        Entity.PictureUrl = new PlacePictureUrl(domainEvent.PlacePictureUrl);
    }
}