using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Management.Domain.Accounts;
using EasyRent.Management.Domain.Accounts.ValueObjects;
using EasyRent.Management.Integrations.Management.Factories;
using EasyRent.MessageBroker;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Management.Core.Handlers.Accounts.Command.SuspendAccount;

public class SuspendAccountCommandHandler : IRequestHandler<SuspendAccountCommand, SuspendAccountResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IMessagePublisher _messagePublisher;

    public SuspendAccountCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IMessagePublisher messagePublisher)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _messagePublisher = messagePublisher;
    }

    public async Task<SuspendAccountResponse> Handle(SuspendAccountCommand request, CancellationToken cancellationToken)
    {
        if (request.AccountId == request.CurrentAccountId)
            throw new PermissionException("You cannot suspend yourself");

        if (!await _aggregateRepository.ExistsAsync<ManagementAccount, AccountId>(new AccountId(request.AccountId)))
            throw new EntityNotFoundException(request.AccountId, typeof(ManagementAccount));

        var account = await _aggregateRepository.LoadAsync<ManagementAccount, AccountId>(
            new AccountId(request.AccountId));

        account.Suspend();

        await _aggregateRepository.SaveAsync<ManagementAccount, AccountId>(account);

        await _messagePublisher.PublishMessageAsync(ManagementIntegrationEventFactory.ExchangeName,
            ManagementIntegrationEventFactory.Suspend(account.Id),
            cancellationToken);

        _logger.Info("Account with ID: {AccountId} updated in the event stream",
            account.Id.Value);

        return new SuspendAccountResponse();
    }
}