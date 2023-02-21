using System.Threading;
using System.Threading.Tasks;
using EasyRent.Identity.Core.Abstractions;
using EasyRent.Identity.Domain.Account;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Query.GetAccountById;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, GetAccountByIdResponse>
{
    private readonly ILogger _logger;
    private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;

    public GetAccountByIdQueryHandler(ILogger logger,
        IAccountReadOnlyRepository accountReadOnlyRepository)
    {
        _logger = logger;
        _accountReadOnlyRepository = accountReadOnlyRepository;
    }

    public async Task<GetAccountByIdResponse> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _accountReadOnlyRepository.GetAccountByIdAsync(request.AccountId, cancellationToken)
                      ?? throw new EntityNotFoundException(request.AccountId, typeof(Account));

        _logger.Info("account with ID: #{AccountId} fetched", request.AccountId);

        return new GetAccountByIdResponse
        {
            Account = account
        };
    }
}