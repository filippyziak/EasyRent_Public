using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.States;

namespace EasyRent.Identity.Domain.Account.Handlers;

public sealed class AccountArchivedEventHandler : DomainEventHandler<Account, AccountArchived>
{
    public AccountArchivedEventHandler(Account entity) : base(entity)
    {
    }

    public override void Handle(AccountArchived domainEvent)
    {
        Entity.State = AccountState.Archived;
    }
}