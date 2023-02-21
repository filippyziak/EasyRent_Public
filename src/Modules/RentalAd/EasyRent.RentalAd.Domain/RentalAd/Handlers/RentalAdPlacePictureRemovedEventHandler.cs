using System.Linq;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlacePictureRemovedEventHandler : DomainEventHandler<RentalAd, RentalAdPlacePictureRemoved>
{
    public RentalAdPlacePictureRemovedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlacePictureRemoved domainEvent)
    {
        var picture = Entity.PlacePictures.FirstOrDefault(p => p.PlacePictureId == domainEvent.PictureId);
        Entity.PlacePictures.Remove(picture);
    }
}