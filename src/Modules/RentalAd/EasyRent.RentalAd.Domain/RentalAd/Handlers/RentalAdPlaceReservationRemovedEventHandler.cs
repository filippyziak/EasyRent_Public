using System.Linq;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceReservationRemovedEventHandler : DomainEventHandler<RentalAd, RentalAdPlaceReservationRemoved>
{
    public RentalAdPlaceReservationRemovedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceReservationRemoved domainEvent)
    {
        var placeReservationToRemove = Entity.PlaceReservations.FirstOrDefault(p
            => p.PlaceReservationId == domainEvent.PlaceReservationId);

        Entity.PlaceReservations.Remove(placeReservationToRemove);
    }
}