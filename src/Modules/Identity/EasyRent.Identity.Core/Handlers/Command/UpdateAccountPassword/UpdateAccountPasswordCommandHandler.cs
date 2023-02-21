using System.Threading;
using System.Threading.Tasks;
using EasyRent.EventSourcing;
using EasyRent.Identity.Domain.Account;
using EasyRent.Identity.Domain.Account.ValueObjects;
using EasyRent.Identity.Domain.DomainServices;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.UpdateAccountPassword;

public class UpdateAccountPasswordCommandHandler : IRequestHandler<UpdateAccountPasswordCommand, UpdateAccountPasswordResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IHashProvider _hashProvider;

    public UpdateAccountPasswordCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IHashProvider hashProvider)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _hashProvider = hashProvider;
    }

    public async Task<UpdateAccountPasswordResponse> Handle(UpdateAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!await _aggregateRepository.ExistsAsync<Account, AccountId>(new AccountId(request.AccountId)))
            throw new EntityNotFoundException(request.AccountId, typeof(Account));

        var account = await _aggregateRepository.LoadAsync<Account, AccountId>(
            new AccountId(request.AccountId));

        account.UpdatePasswordData(AccountPasswordData.FromString(request.NewPassword, _hashProvider));

        await _aggregateRepository.SaveAsync<Account, AccountId>(account);

        _logger.Info("Account with ID: {AccountId} archived in the event stream",
            account.Id.Value);

        return new UpdateAccountPasswordResponse();
    }
}