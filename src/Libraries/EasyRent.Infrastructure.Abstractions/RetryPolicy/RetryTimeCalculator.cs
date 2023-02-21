using System;

namespace EasyRent.Infrastructure.Abstractions.RetryPolicy;

public static class RetryTimeCalculator
{
    public static TimeSpan ExponentialRetryTimeSpan(int retryAttempt)
        => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
}