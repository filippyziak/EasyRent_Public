using EasyRent.Identity.ReadModels.ReadModels;
using EasyRent.Shared.Models;
using EasyRent.Shared.Pagination;

namespace EasyRent.Identity.Core.Handlers.Query.GetAccounts;

public record GetAccountsResponse(Error Error = null) : BaseResponse(Error)
{
    public IPagedList<AccountReadModel> Accounts { get; init; } = PagedList<AccountReadModel>.Empty;
}