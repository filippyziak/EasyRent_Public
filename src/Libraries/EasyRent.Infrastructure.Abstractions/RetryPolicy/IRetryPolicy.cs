using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRent.Infrastructure.Abstractions.RetryPolicy;

public interface IRetryPolicy
{
    string PolicyKey { get; }

    Task ExecutePolicyAsync(Func<CancellationToken, Task> execute,
        int retriesCount,
        CancellationToken cancellationToken = default);
}