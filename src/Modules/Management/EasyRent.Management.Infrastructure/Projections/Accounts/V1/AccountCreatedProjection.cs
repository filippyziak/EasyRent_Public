using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Management.Domain.Accounts.DomainEvents.V1;
using EasyRent.Management.Domain.Accounts.States;
using EasyRent.Management.Infrastructure.DocumentStore.Documents;
using EasyRent.Management.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.Management.Infrastructure.Projections.Accounts.V1;

public class AccountCreatedProjection : DefaultProjection<AccountCreated>
{
    public AccountCreatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(AccountCreated @event)
    {
        var documentRepository = Scope.ResolveService<IManagementAccountDocumentRepository>();

        if (!await documentRepository.ExistsAsync(a => a.AccountId == @event.AccountId))
        {
            await documentRepository.StoreAsync(new ManagementAccountDocument
            {
                AccountId = @event.AccountId,
                Type = @event.Type,
                State = AccountState.Active.ToString()
            });

            Logger.Trace("> Account with ID: #{AccountId} stored in the document store", @event.AccountId);
        }
    }
}