using EasyRent.EventSourcing;
using EasyRent.Management.Domain.Accounts.DomainEvents.V1;
using EasyRent.Management.Domain.Accounts.ValueObjects;

namespace EasyRent.Management.Domain.Accounts.Handlers;

public class AccountTypeChangedEventHandler : DomainEventHandler<ManagementAccount, AccountTypeChanged>
{
    public AccountTypeChangedEventHandler(ManagementAccount entity) : base(entity)
    {
    }

    public override void Handle(AccountTypeChanged domainEvent)
    {
        Entity.Type = new AccountType(domainEvent.Type);
    }
}