using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Identity.Core.Abstractions;
using EasyRent.Identity.Domain.Account;
using EasyRent.Identity.Domain.Account.ValueObjects;
using EasyRent.Identity.Integrations.Identity.Factories;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.MessageBroker;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.UpdateAccountEmailAddress;

public class UpdateAccountEmailAddressCommandHandler : IRequestHandler<UpdateAccountEmailAddressCommand, UpdateAccountEmailAddressResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
    private readonly IMessagePublisher _messagePublisher;

    public UpdateAccountEmailAddressCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IAccountReadOnlyRepository accountReadOnlyRepository,
        IMessagePublisher messagePublisher)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _accountReadOnlyRepository = accountReadOnlyRepository;
        _messagePublisher = messagePublisher;
    }

    public async Task<UpdateAccountEmailAddressResponse> Handle(UpdateAccountEmailAddressCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Account, AccountId>(new AccountId(request.AccountId)))
        {
            throw new EntityNotFoundException(request.AccountId, typeof(Account));
        }

        if (await _accountReadOnlyRepository.GetAccountIdByEmailAddressAsync(request.NewEmailAddress) is not null)
        {
            throw new EntityAlreadyExistsException(typeof(Account));
        }

        var account = await _aggregateRepository.LoadAsync<Account, AccountId>(
            new AccountId(request.AccountId));

        account.UpdateEmail(AccountEmailAddress.FromString(request.NewEmailAddress));

        await _aggregateRepository.SaveAsync<Account, AccountId>(account);

        await _messagePublisher.PublishMessageAsync(IdentityIntegrationEventFactory.ExchangeName,
            IdentityIntegrationEventFactory.Update(account.Id, request.NewEmailAddress));

        _logger.Info("Account with ID: {AccountId} archived in the event stream",
            account.Id.Value);

        return new UpdateAccountEmailAddressResponse();
    }
}