using EasyRent.Identity.Core.Abstractions;
using EasyRent.Identity.ReadModels.ReadModels;
using EasyRent.Identity.Shared.Abstractions;

namespace EasyRent.Identity.Infrastructure.Facades;

public class IdentityContextFacade : IIdentityContextFacade
{
    private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;

    public IdentityContextFacade(IAccountReadOnlyRepository accountReadOnlyRepository)
    {
        _accountReadOnlyRepository = accountReadOnlyRepository;
    }

    public Task<AccountReadModel> GetGetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        => _accountReadOnlyRepository.GetAccountByIdAsync(accountId, cancellationToken);
}