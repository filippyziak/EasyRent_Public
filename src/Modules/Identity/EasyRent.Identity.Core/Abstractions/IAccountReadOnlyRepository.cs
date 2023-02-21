using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Identity.Core.Handlers.Query.GetAccounts;
using EasyRent.Identity.ReadModels.ReadModels;
using EasyRent.Shared.Pagination;

namespace EasyRent.Identity.Core.Abstractions;

public interface IAccountReadOnlyRepository
{
    Task<IPagedList<AccountReadModel>> GetAccountsAsync(GetAccountsQuery query, CancellationToken cancellationToken = default);
    Task<AccountReadModel> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default);
    Task<Guid?> GetAccountIdByEmailAddressAsync(string emailAddress, CancellationToken cancellationToken = default);
}