using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyRent.EventSourcing;
using EasyRent.Identity.Core.Abstractions;
using EasyRent.Identity.Domain.Account;
using EasyRent.Identity.Domain.Account.ValueObjects;
using EasyRent.Identity.Domain.DomainServices;
using EasyRent.Identity.Domain.Exceptions;
using EasyRent.Identity.ReadModels.ReadModels;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Shared.Exceptions;
using MediatR;

namespace EasyRent.Identity.Core.Handlers.Command.LoginAccount;

public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, LoginAccountResponse>
{
    private readonly ILogger _logger;
    private readonly IAggregateRepository _aggregateRepository;
    private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
    private readonly IAuthTokenProvider _authTokenProvider;
    private readonly IHashProvider _hashProvider;
    private readonly IMapper _mapper;

    public LoginAccountCommandHandler(ILogger logger,
        IAggregateRepository aggregateRepository,
        IAccountReadOnlyRepository accountReadOnlyRepository,
        IAuthTokenProvider authTokenProvider,
        IHashProvider hashProvider,
        IMapper mapper)
    {
        _logger = logger;
        _aggregateRepository = aggregateRepository;
        _accountReadOnlyRepository = accountReadOnlyRepository;
        _authTokenProvider = authTokenProvider;
        _hashProvider = hashProvider;
        _mapper = mapper;
    }

    public async Task<LoginAccountResponse> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = await _accountReadOnlyRepository.GetAccountIdByEmailAddressAsync(request.EmailAddress, cancellationToken)
                     ?? throw new EntityNotFoundException(typeof(Account));

        var account = await _aggregateRepository.LoadAsync<Account, AccountId>(new AccountId(userId))
                      ?? throw new InvalidCredentialsException();

        account.Login(AccountPasswordData.FromStringWithSalt(request.Password, account.PasswordData.PasswordSalt, _hashProvider));

        var accountReadModel = _mapper.Map<AccountReadModel>(account);

        var jwtToken = _authTokenProvider.CreateToken(accountReadModel);

        _logger.Info("Account with ID: {AccountId} logged in successfully",
            account.Id.Value);

        return new LoginAccountResponse
        {
            JwtToken = jwtToken,
            Account = accountReadModel
        };
    }
}