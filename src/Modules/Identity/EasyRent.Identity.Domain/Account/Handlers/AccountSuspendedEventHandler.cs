using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.States;

namespace EasyRent.Identity.Domain.Account.Handlers;

public class AccountSuspendedEventHandler : DomainEventHandler<Account,AccountSuspended>
{
    public AccountSuspendedEventHandler(Account entity) : base(entity)
    {
    }

    public override void Handle(AccountSuspended domainEvent)
    {
        Entity.State = AccountState.Suspended;
    }
}