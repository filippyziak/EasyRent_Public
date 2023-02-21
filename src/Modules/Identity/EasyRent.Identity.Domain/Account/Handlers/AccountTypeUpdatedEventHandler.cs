using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.ValueObjects;

namespace EasyRent.Identity.Domain.Account.Handlers;

public class AccountTypeUpdatedEventHandler : DomainEventHandler<Account, AccountTypeUpdated>
{
    public AccountTypeUpdatedEventHandler(Account entity) : base(entity)
    {
    }

    public override void Handle(AccountTypeUpdated domainEvent)
    {
        Entity.Type = AccountType.FromString(domainEvent.AccountType);
    }
}