using EasyRent.EventSourcing;
using EasyRent.Management.Domain.Accounts.DomainEvents.V1;
using EasyRent.Management.Domain.Accounts.States;

namespace EasyRent.Management.Domain.Accounts.Handlers;

public class AccountArchivedEventHandler : DomainEventHandler<ManagementAccount, AccountArchived>
{
    public AccountArchivedEventHandler(ManagementAccount entity) : base(entity)
    {
    }

    public override void Handle(AccountArchived domainEvent)
    {
        Entity.State = AccountState.Archived;
    }
}