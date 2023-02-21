using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.States;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public class RentalAdPlaceReservationFinishedEventHandler : DomainEventHandler<PlaceReservation, RentalAdPlaceReservationFinished>
{
    public RentalAdPlaceReservationFinishedEventHandler(PlaceReservation entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPlaceReservationFinished domainEvent)
    {
        Entity.State = PlaceReservationState.Finished;
    }
}