using EasyRent.Shared.Pagination;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Query.GetAccounts;

public record GetAccountsQuery : PaginationQuery, IRequest<GetAccountsResponse>;