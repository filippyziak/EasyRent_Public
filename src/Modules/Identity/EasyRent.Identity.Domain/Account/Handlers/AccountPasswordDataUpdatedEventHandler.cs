using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.ValueObjects;

namespace EasyRent.Identity.Domain.Account.Handlers;

public sealed class AccountPasswordDataUpdatedEventHandler : DomainEventHandler<Account, AccountPasswordDataUpdated>
{
    public AccountPasswordDataUpdatedEventHandler(Account entity) : base(entity)
    {
    }

    public override void Handle(AccountPasswordDataUpdated domainEvent)
    {
        Entity.PasswordData = new AccountPasswordData(domainEvent.NewPasswordHash, domainEvent.NewPasswordSalt);
    }
}