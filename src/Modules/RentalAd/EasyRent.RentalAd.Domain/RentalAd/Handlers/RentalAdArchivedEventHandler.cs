using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.States;

namespace EasyRent.RentalAd.Domain.RentalAd.Handlers;

public sealed class RentalAdArchivedEventHandler : DomainEventHandler<RentalAd, RentalAdArchived>
{
    public RentalAdArchivedEventHandler(RentalAd entity) : base(entity)
    {
    }

    public override void Handle(RentalAdArchived domainEvent)
    {
        Entity.State = RentalAdState.Archived;
    }
}