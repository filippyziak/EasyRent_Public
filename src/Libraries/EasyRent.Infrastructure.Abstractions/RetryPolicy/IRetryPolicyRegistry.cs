namespace EasyRent.Infrastructure.Abstractions.RetryPolicy;

public interface IRetryPolicyRegistry
{
    IRetryPolicy GetPolicy(string policyKey);
}