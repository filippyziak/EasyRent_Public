using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Management.Domain.Accounts;
using EasyRent.Management.Domain.Accounts.ValueObjects;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, DeleteAccountResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public DeleteAccountCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<DeleteAccountResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<ManagementAccount, AccountId>(new AccountId(request.AccountId)))
            throw new EntityNotFoundException(request.AccountId, typeof(ManagementAccount));

        var account = await _aggregateRepository.LoadAsync<ManagementAccount, AccountId>(
            new AccountId(request.AccountId));

        account.Archive();

        await _aggregateRepository.SaveAsync<ManagementAccount, AccountId>(account);

        _logger.Info("Account with ID: {AccountId} updated in the event stream",
            account.Id.Value);

        return new DeleteAccountResponse();
    }
}