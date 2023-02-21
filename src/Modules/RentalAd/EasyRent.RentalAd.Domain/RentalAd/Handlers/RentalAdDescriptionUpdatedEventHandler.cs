using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdDescriptionUpdatedEventHandler : DomainEventHandler<RentalAd, RentalAdDescriptionUpdated>
{
    public RentalAdDescriptionUpdatedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdDescriptionUpdated domainEvent)
    {
        Entity.Description = new RentalAdDescription(domainEvent.Description);
    }
}