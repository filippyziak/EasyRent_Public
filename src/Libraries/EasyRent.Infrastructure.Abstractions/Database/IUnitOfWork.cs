using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRent.Infrastructure.Abstractions.Database;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}