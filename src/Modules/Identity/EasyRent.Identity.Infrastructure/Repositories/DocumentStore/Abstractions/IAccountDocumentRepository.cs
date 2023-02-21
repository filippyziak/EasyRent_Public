using EasyRent.Identity.Core.Handlers.Query.GetAccounts;
using EasyRent.Identity.Infrastructure.DocumentStore.Documents;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Shared.Pagination;

namespace EasyRent.Identity.Infrastructure.Repositories.DocumentStore.Abstractions;

public interface IAccountDocumentRepository : IDocumentRepository<AccountDocument>
{
    Task<IPagedList<AccountDocument>> GetAccountsAsync(GetAccountsQuery query, CancellationToken cancellationToken);
}