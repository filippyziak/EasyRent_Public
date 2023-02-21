using EasyRent.EventSourcing;
using EasyRent.Management.Domain.Accounts.DomainEvents.V1;
using EasyRent.Management.Domain.Accounts.States;
using EasyRent.Management.Domain.Accounts.ValueObjects;
using EasyRent.Shared.Exceptions;

namespace EasyRent.Management.Domain.Accounts;

public class ManagementAccount : AggregateRoot<AccountId>
{
    public AccountId Id { get; internal set; }
    public AccountType Type { get; internal set; }
    public AccountState State { get; set; }

    protected ManagementAccount()
    {
    }

    public ManagementAccount(AccountId id,
        AccountType type)
        => Apply(new AccountCreated(id, type));

    public void Activate()
    {
        if (State == AccountState.Active)
        {
            return;
        }

        EnsureNotArchived();

        Apply(new AccountActivated(Id));
    }

    public void Suspend()
    {
        if (State == AccountState.Suspended)
        {
            return;
        }

        EnsureNotArchived();

        Apply(new AccountSuspended(Id));
    }

    public void ChangeAccountType(AccountType type)
    {
        EnsureNotArchived();

        Apply(new AccountTypeChanged(Id, type));
    }

    public void Archive()
        => Apply(new AccountArchived(Id));

    protected override void EnsureValidState()
    {
        var valid = Id is not null
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
}