using EasyRent.EventSourcing;
using EasyRent.Management.Domain.Accounts.DomainEvents.V1;
using EasyRent.Management.Domain.Accounts.States;

namespace EasyRent.Management.Domain.Accounts.Handlers;

public class AccountActivatedEventHandler : DomainEventHandler<ManagementAccount, AccountActivated>
{
    public AccountActivatedEventHandler(ManagementAccount entity) : base(entity)
    {
    }

    public override void Handle(AccountActivated domainEvent)
    {
        Entity.State = AccountState.Active;
    }
}