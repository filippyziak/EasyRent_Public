using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Infrastructure.Abstractions.RetryPolicy;
using Polly;

namespace EasyRent.Infrastructure.RetryPolicies;

public class DefaultPollyRetryPolicy: IDefaultRetryPolicy
{
    protected readonly ILogger Logger;

    public DefaultPollyRetryPolicy(ILogger logger)
        => Logger = logger;

    public string PolicyKey => "DEFAULT";

    public Task ExecutePolicyAsync(Func<CancellationToken, Task> execute,
        int retriesCount,
        CancellationToken cancellationToken = default)
        => PrepareRetryPolicy(Logger, retriesCount)
            .ExecuteAsync(execute, cancellationToken);

    protected virtual IAsyncPolicy PrepareRetryPolicy(ILogger logger, int retriesCount) => Policy
        .Handle<Exception>()
        .WaitAndRetryAsync(retriesCount, RetryTimeCalculator.ExponentialRetryTimeSpan,
            (exception, timespan, retryAttempt, _) =>
            {
                logger.Warning("An error occurred during processing a request. Exception: {ExceptionMessage}", exception.Message);
                logger.Warning("Retry attempt: {RetryAttempt}. Waiting {TimeSpan} for the next attempt",
                    retryAttempt,
                    timespan);
            });
}