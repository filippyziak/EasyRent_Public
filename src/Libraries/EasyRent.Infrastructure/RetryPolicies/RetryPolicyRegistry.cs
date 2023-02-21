using System;
using System.Collections.Generic;
using System.Linq;
using EasyRent.Infrastructure.Abstractions.RetryPolicy;

namespace EasyRent.Infrastructure.RetryPolicies;

public class RetryPolicyRegistry : IRetryPolicyRegistry
{
    private readonly IReadOnlyList<IRetryPolicy> _retryPolicies;

    public RetryPolicyRegistry(IEnumerable<IRetryPolicy> retryPolicies)
        => _retryPolicies = retryPolicies.ToList();

    public IRetryPolicy GetPolicy(string policyKey)
        => _retryPolicies.SingleOrDefault(policy => policy.PolicyKey.Equals(policyKey, StringComparison.InvariantCultureIgnoreCase))
           ?? throw new KeyNotFoundException($"Retry policy with key: '{policyKey}' not registered");
}