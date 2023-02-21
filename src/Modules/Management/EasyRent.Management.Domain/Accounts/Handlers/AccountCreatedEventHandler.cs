using EasyRent.EventSourcing;
using EasyRent.Management.Domain.Accounts.DomainEvents.V1;
using EasyRent.Management.Domain.Accounts.States;
using EasyRent.Management.Domain.Accounts.ValueObjects;

namespace EasyRent.Management.Domain.Accounts.Handlers;

public class AccountCreatedEventHandler : DomainEventHandler<ManagementAccount, AccountCreated>
{
    public AccountCreatedEventHandler(ManagementAccount entity) : base(entity)
    {
    }

    public override void Handle(AccountCreated domainEvent)
    {
        Entity.Id = new AccountId(domainEvent.AccountId);
        Entity.Type = new AccountType(domainEvent.Type);
        Entity.State = AccountState.Active;
    }
}