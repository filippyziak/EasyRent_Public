using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdCreatedEventHandler : DomainEventHandler<RentalAd, RentalAdCreated>
{
    public RentalAdCreatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdCreated domainEvent)
    {
        Entity.Id = new RentalAdId(domainEvent.RentalAdId);
        Entity.Title = new RentalAdTitle(domainEvent.Title);
        Entity.Description = new RentalAdDescription(domainEvent.Description);
        Entity.PricePerDay = new RentalAdPricePerDay(domainEvent.PricePerDay);
    }
}