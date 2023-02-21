using EasyRent.EventSourcing;
using EasyRent.Management.Domain.Accounts.DomainEvents.V1;
using EasyRent.Management.Domain.Accounts.States;

namespace EasyRent.Management.Domain.Accounts.Handlers;

public class AccountSuspendedEventHandler : DomainEventHandler<ManagementAccount, AccountSuspended>
{
    public AccountSuspendedEventHandler(ManagementAccount entity) : base(entity)
    {
    }

    public override void Handle(AccountSuspended domainEvent)
    {
        Entity.State = AccountState.Suspended;
    }
}