using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Management.Domain.Accounts.DomainEvents.V1;
using EasyRent.Management.Domain.Accounts.States;
using EasyRent.Management.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.Management.Infrastructure.Projections.Accounts.V1;

public class AccountActivatedProjection : DefaultProjection<AccountActivated>
{
    public AccountActivatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(AccountActivated @event)
    {
        var documentRepository = Scope.ResolveService<IManagementAccountDocumentRepository>();
        var account = await documentRepository.FindAsync(a => a.AccountId == @event.AccountId);

        if (account is not null)
        {
            account.State = AccountState.Active.ToString();
            await documentRepository.ReplaceAsync(account);

            Logger.Trace("> Account with ID: #{AccountId} updated in the document store", @event.AccountId);
        }
    }
}