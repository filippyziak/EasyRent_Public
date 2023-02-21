using System.Linq;
using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.States;
using EasyRent.Identity.Domain.Account.ValueObjects;
using EasyRent.Identity.Domain.Exceptions;
using EasyRent.Shared.Exceptions;

namespace EasyRent.Identity.Domain.Account;

public class Account : AggregateRoot<AccountId>
{
    public AccountId Id { get; internal set; }
    public AccountEmailAddress EmailAddress { get; internal set; }
    public AccountPasswordData PasswordData { get; internal set; }
    public AccountType Type { get; internal set; }
    public AccountState State { get; internal set; }

    protected Account()
    {
    }

    public Account(AccountId id,
        AccountEmailAddress emailAddress,
        AccountPasswordData passwordData,
        AccountType type)
        => Apply(new AccountCreated(id,
            emailAddress,
            passwordData.PasswordHash,
            passwordData.PasswordSalt,
            type));

    public void Login(AccountPasswordData passwordData)
    {
        EnsureActive();

        if (!passwordData.PasswordHash.SequenceEqual(PasswordData.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }
    }

    public void UpdateEmail(AccountEmailAddress emailAddress)
    {
        EnsureActive();
        Apply(new AccountEmailAddressUpdated(Id, emailAddress));
    }

    public void UpdatePasswordData(AccountPasswordData passwordData)
    {
        EnsureActive();
        Apply(new AccountPasswordDataUpdated(Id, passwordData.PasswordSalt, passwordData.PasswordHash));
    }
    
    public void UpdateAccountType(AccountType accountType)
    {
        EnsureActive();
        Apply(new AccountTypeUpdated(Id, accountType));
    }

    public void Archive()
    {
        EnsureNotArchived();
        Apply(new AccountArchived(Id));
    }
    
    public void Suspend()
    {
        EnsureActive();
        Apply(new AccountSuspended(Id));
    }

    public void Activate()
    {
        if (State == AccountState.Active)
        {
            return;
        }

        EnsureNotArchived();

        Apply(new AccountActivated(Id));
    }

    protected override void EnsureValidState()
    {
        var valid = Id is not null
                    && EmailAddress is not null
                    && PasswordData is not null
                    && Type is not null;

        if (!valid)
            throw new InvalidEntityStateException(this, $"Account validation failed");
    }

    private void EnsureNotArchived()
    {
        if (State == AccountState.Archived)
        {
            throw new InvalidEntityStateException(this, "Account is archived");
        }
    }
    
    private void EnsureActive()
    {
        if (State != AccountState.Active)
        {
            throw new InvalidEntityStateException(this, "Account is not active");
        }
    }
}