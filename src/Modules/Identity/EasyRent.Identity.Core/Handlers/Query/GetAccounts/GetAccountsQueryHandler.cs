using System.Threading;
using System.Threading.Tasks;
using EasyRent.Identity.Core.Abstractions;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Query.GetAccounts;

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, GetAccountsResponse>
{
    private readonly ILogger _logger;
    private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;

    public GetAccountsQueryHandler(ILogger logger,
        IAccountReadOnlyRepository accountReadOnlyRepository)
    {
        _logger = logger;
        _accountReadOnlyRepository = accountReadOnlyRepository;
    }

    public async Task<GetAccountsResponse> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _accountReadOnlyRepository.GetAccountsAsync(request, cancellationToken);

        _logger.Info("{AccountsCount} accounts fetched", accounts.TotalCount);

        return new GetAccountsResponse
        {
            Accounts = accounts
        };
    }
}