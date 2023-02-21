using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Domain.Account.States;
using EasyRent.Identity.Infrastructure.DocumentStore.Documents;
using EasyRent.Identity.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Infrastructure.Abstractions.Abstractions;

namespace EasyRent.Identity.Infrastructure.Projections.Account.V1;

public class AccountCreatedProjection : DefaultProjection<AccountCreated>
{
    public AccountCreatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(AccountCreated @event)
    {
        var documentRepository = Scope.ResolveService<IAccountDocumentRepository>();

        if (!await documentRepository.ExistsAsync(a => a.AccountId == @event.AccountId.ToString()))
        {
            await documentRepository.StoreAsync(new AccountDocument
            {
                AccountId = @event.AccountId.ToString(),
                EmailAddress = @event.EmailAddress,
                PasswordHash = @event.PasswordHash,
                PasswordSalt = @event.PasswordSalt,
                AccountType = @event.AccountType,
                State = AccountState.Active.ToString()
            });

            Logger.Trace("> Account with ID: #{AccountId} stored in the document store", @event.AccountId);
        }
    }
}