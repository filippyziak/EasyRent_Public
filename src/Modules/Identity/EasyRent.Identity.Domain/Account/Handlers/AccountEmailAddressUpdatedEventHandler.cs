using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.ValueObjects;

namespace EasyRent.Identity.Domain.Account.Handlers;

public sealed class AccountEmailAddressUpdatedEventHandler : DomainEventHandler<Account, AccountEmailAddressUpdated>
{
    public AccountEmailAddressUpdatedEventHandler(Account entity) : base(entity)
    {
    }

    public override void Handle(AccountEmailAddressUpdated domainEvent)
    {
        Entity.EmailAddress = AccountEmailAddress.FromString(domainEvent.NewEmailAddress);
    }
}