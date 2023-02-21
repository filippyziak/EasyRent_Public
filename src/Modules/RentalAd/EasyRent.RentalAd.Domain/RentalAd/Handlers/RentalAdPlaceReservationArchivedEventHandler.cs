using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.States;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceReservationArchivedEventHandler : DomainEventHandler<PlaceReservation, RentalAdPlaceReservationArchived>
{
    public RentalAdPlaceReservationArchivedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceReservationArchived domainEvent)
    {
        Entity.State = PlaceReservationState.Archived;
    }
}