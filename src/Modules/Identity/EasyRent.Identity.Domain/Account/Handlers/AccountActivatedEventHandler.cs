using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.States;

namespace EasyRent.Identity.Domain.Account.Handlers;

public class AccountActivatedEventHandler : DomainEventHandler<Account, AccountActivated>
{
    public AccountActivatedEventHandler(Account entity) : base(entity)
    {
    }

    public override void Handle(AccountActivated domainEvent)
    {
        Entity.State = AccountState.Active;
    }
}