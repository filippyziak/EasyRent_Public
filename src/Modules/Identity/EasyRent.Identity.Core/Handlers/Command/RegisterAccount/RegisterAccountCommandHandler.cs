using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Identity.Core.Abstractions;
using EasyRent.Identity.Domain.Account;
using EasyRent.Identity.Domain.Account.ValueObjects;
using EasyRent.Identity.Domain.DomainServices;
using EasyRent.Identity.Integrations.Identity.Factories;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.MessageBroker;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.RegisterAccount;

public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, RegisterAccountResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
    private readonly IHashProvider _hashProvider;
    private readonly IMessagePublisher _messagePublisher;

    public RegisterAccountCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IAccountReadOnlyRepository accountReadOnlyRepository,
        IHashProvider hashProvider,
        IMessagePublisher messagePublisher)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _accountReadOnlyRepository = accountReadOnlyRepository;
        _hashProvider = hashProvider;
        _messagePublisher = messagePublisher;
    }

    public async Task<RegisterAccountResponse> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        if (await _accountReadOnlyRepository.GetAccountIdByEmailAddressAsync(request.EmailAddress) != null)
        {
            throw new EntityAlreadyExistsException( typeof(Account));
        }

        var account = new Account(new AccountId(Guid.NewGuid()),
            AccountEmailAddress.FromString(request.EmailAddress),
            AccountPasswordData.FromString(request.Password, _hashProvider),
            AccountType.FromEnum(request.AccountType));

        await _aggregateRepository.SaveAsync<Account, AccountId>(account);

        await _messagePublisher.PublishMessageAsync(IdentityIntegrationEventFactory.ExchangeName,
            IdentityIntegrationEventFactory.Create(account.Id,
                account.EmailAddress,
                account.Type));

        _logger.Info("Account with ID: {AccountId} stored in the event stream",
            account.Id.Value);

        return new RegisterAccountResponse();
    }
}