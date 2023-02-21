using System.Threading;
using EasyRent.Identity.ReadModels.ReadModels;

namespace EasyRent.Identity.Core.Abstractions;

public interface IAuthTokenProvider
{
    string CreateToken(AccountReadModel userAccount, CancellationToken cancellationToken = default);
}