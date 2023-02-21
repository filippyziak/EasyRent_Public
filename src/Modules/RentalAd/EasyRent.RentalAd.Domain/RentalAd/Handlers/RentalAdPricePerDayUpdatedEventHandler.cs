using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdPricePerDayUpdatedEventHandler : DomainEventHandler<RentalAd, RentalAdPricePerDayUpdated>
{
    public RentalAdPricePerDayUpdatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdPricePerDayUpdated domainEvent)
    {
        Entity.PricePerDay = RentalAdPricePerDay.FromDecimal(domainEvent.PricePerDay);
    }
}