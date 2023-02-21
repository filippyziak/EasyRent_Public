using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Identity.ReadModels.ReadModels;

namespace EasyRent.Identity.Shared.Abstractions;

public interface IIdentityContextFacade
{
    Task<AccountReadModel> GetGetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default);
}