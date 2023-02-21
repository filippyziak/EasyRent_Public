using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdMainPictureRemovedEventHandler : DomainEventHandler<RentalAd, RentalAdMainPictureRemoved>
{
    public RentalAdMainPictureRemovedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdMainPictureRemoved domainEvent)
    {
        Entity.MainPlacePicture = null;
    }
}