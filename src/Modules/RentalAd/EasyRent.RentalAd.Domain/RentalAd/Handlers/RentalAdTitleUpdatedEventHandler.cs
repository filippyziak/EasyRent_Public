using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdTitleUpdatedEventHandler : DomainEventHandler<RentalAd, RentalAdTitleUpdated>
{
    public RentalAdTitleUpdatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdTitleUpdated domainEvent)
    {
        Entity.Title = new RentalAdTitle(domainEvent.Title);
    }
}