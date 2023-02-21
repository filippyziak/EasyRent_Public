using AutoMapper;
using EasyRent.Identity.Core.Abstractions;
using EasyRent.Identity.Core.Handlers.Query.GetAccounts;
using EasyRent.Identity.Infrastructure.DocumentStore.Documents;
using EasyRent.Identity.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.Identity.ReadModels.ReadModels;
using EasyRent.Infrastructure.Extensions;
using EasyRent.Shared.Pagination;

namespace EasyRent.Identity.Infrastructure.Repositories.Core;

public class AccountReadOnlyRepository : IAccountReadOnlyRepository
{
    private readonly IAccountDocumentRepository _accountDocumentRepository;
    private readonly IMapper _mapper;

    public AccountReadOnlyRepository(IAccountDocumentRepository accountDocumentRepository,
        IMapper mapper)
    {
        _accountDocumentRepository = accountDocumentRepository;
        _mapper = mapper;
    }

    public async Task<IPagedList<AccountReadModel>> GetAccountsAsync(GetAccountsQuery query, CancellationToken cancellationToken = default)
        => _mapper.ToPagedList<AccountReadModel, AccountDocument>(await _accountDocumentRepository.GetAccountsAsync(query, cancellationToken));

    public async Task<AccountReadModel> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        => _mapper.Map<AccountReadModel>(await _accountDocumentRepository.FindAsync(a => a.AccountId == accountId.ToString()));

    public async Task<Guid?> GetAccountIdByEmailAddressAsync(string emailAddress, CancellationToken cancellationToken = default)
    {
        var accountId = (await _accountDocumentRepository.FindAsync(a
            => a.EmailAddress == emailAddress))?.AccountId;

        return accountId is null 
            ? null 
            : new Guid(accountId);
    }
}