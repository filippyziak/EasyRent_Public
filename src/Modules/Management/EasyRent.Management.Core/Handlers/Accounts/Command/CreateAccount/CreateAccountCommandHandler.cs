using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Management.Domain.Accounts;
using EasyRent.Management.Domain.Accounts.ValueObjects;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;

    public CreateAccountCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
    }

    public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        if (await _aggregateRepository.ExistsAsync<ManagementAccount, AccountId>(new AccountId(request.AccountId)))
            throw new EntityAlreadyExistsException(request.AccountId, typeof(ManagementAccount));

        var account = new ManagementAccount(new AccountId(request.AccountId),
            AccountType.FromEnum(request.Type));

        await _aggregateRepository.SaveAsync<ManagementAccount, AccountId>(account);

        _logger.Info("Account with ID: {AccountId} stored in the event stream",
            account.Id.Value);

        return new CreateAccountResponse();
    }
}