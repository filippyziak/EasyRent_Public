using System.Linq;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.States;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdMainPictureSetEventHandler : DomainEventHandler<RentalAd, RentalAdMainPictureSet>
{
    public RentalAdMainPictureSetEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdMainPictureSet domainEvent)
    {
        Entity.MainPlacePicture = Entity.PlacePictures.First(p
            => p.PlacePictureId == domainEvent.PlacePictureId);
    }
}

public sealed class PlacePictureRentalAdMainPictureSetEventHandler : DomainEventHandler<PlacePicture, RentalAdMainPictureSet>
{
    public PlacePictureRentalAdMainPictureSetEventHandler(PlacePicture entity) : base(entity)
    {
    }

    public override void Handle(RentalAdMainPictureSet domainEvent)
    {
        Entity.PlacePictureState = PlacePictureState.MainPlacePicture;
    }
}