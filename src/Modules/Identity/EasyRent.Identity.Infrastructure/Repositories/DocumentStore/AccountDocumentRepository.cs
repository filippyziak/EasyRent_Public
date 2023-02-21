using EasyRent.Identity.Core.Handlers.Query.GetAccounts;
using EasyRent.Identity.Infrastructure.DocumentStore.Documents;
using EasyRent.Identity.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Infrastructure.DocumentStore;
using EasyRent.Infrastructure.Extensions;
using EasyRent.Shared.Pagination;
using MongoDB.Driver;

namespace EasyRent.Identity.Infrastructure.Repositories.DocumentStore;

public class AccountDocumentRepository : MongoDbDocumentRepository<AccountDocument>, IAccountDocumentRepository
{
    public AccountDocumentRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
    {
    }

    public async Task<IPagedList<AccountDocument>> GetAccountsAsync(GetAccountsQuery query, CancellationToken cancellationToken)
        => await Collection.AsQueryable().ToPagedListAsync(query, cancellationToken);
}