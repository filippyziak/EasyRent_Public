using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account;
using EasyRent.Identity.Domain.Account.ValueObjects;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.RemoveAccount;

public class RemoveAccountCommandHandler : IRequestHandler<RemoveAccountCommand, RemoveAccountResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public RemoveAccountCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<RemoveAccountResponse> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Account, AccountId>(new AccountId(request.AccountId)))
            throw new EntityNotFoundException(request.AccountId, typeof(Account));

        var account = await _aggregateRepository.LoadAsync<Account, AccountId>(
            new AccountId(request.AccountId));

        account.Archive();

        await _aggregateRepository.SaveAsync<Account, AccountId>(account);

        _logger.Info("Account with ID: {AccountId} archived in the event stream",
            account.Id.Value);

        return new RemoveAccountResponse();
    }
}