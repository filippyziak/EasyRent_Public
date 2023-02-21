using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account.DomainEvents.V1;
using EasyRent.Identity.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Infrastructure.Abstractions.Abstractions;

namespace EasyRent.Identity.Infrastructure.Projections.Account.V1;

public class AccountPasswordDataUpdatedProjection : DefaultProjection<AccountPasswordDataUpdated>
{
    public AccountPasswordDataUpdatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(AccountPasswordDataUpdated @event)
    {
        var documentRepository = Scope.ResolveService<IAccountDocumentRepository>();
        var account = await documentRepository.FindAsync(a => a.AccountId == @event.AccountId.ToString());

        if (account is not null)
        {
            account.PasswordHash = @event.NewPasswordHash;
            account.PasswordSalt = @event.NewPasswordSalt;
            await documentRepository.ReplaceAsync(account);

            Logger.Trace("> Account with ID: #{AccountId} updated in the document store", @event.AccountId);
        }
    }
}