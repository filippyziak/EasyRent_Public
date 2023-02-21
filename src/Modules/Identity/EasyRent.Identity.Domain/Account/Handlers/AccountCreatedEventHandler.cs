using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.States;
using EasyRent.Identity.Domain.Account.ValueObjects;

namespace EasyRent.Identity.Domain.Account.Handlers;

public sealed class AccountCreatedEventHandler : DomainEventHandler<Account, AccountCreated>
{
    public AccountCreatedEventHandler(Account entity) : base(entity)
    {
    }

    public override void Handle(AccountCreated domainEvent)
    {
        Entity.Id = new AccountId(domainEvent.AccountId);
        Entity.Type = new AccountType(domainEvent.AccountType);
        Entity.EmailAddress = new AccountEmailAddress(domainEvent.EmailAddress);
        Entity.PasswordData = new AccountPasswordData(domainEvent.PasswordHash, domainEvent.PasswordSalt);
        Entity.State = AccountState.Active;
    }
}