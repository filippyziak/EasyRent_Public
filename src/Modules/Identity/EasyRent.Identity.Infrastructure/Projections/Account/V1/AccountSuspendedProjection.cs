using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.States;
using EasyRent.Identity.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Infrastructure.Abstractions.Abstractions;

namespace EasyRent.Identity.Infrastructure.Projections.Account.V1;

public class AccountSuspendedProjection : DefaultProjection<AccountSuspended>
{
    public AccountSuspendedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(AccountSuspended @event)
    {
        var documentRepository = Scope.ResolveService<IAccountDocumentRepository>();
        var account = await documentRepository.FindAsync(a => a.AccountId == @event.AccountId.ToString());

        if (account is not null)
        {
            account.State = AccountState.Suspended.ToString();
            await documentRepository.ReplaceAsync(account);

            Logger.Trace("> Account with ID: #{AccountId} updated in the document store", @event.AccountId);
        }
    }
}