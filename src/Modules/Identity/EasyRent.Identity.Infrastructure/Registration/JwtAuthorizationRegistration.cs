using EasyRent.Identity.Domain.Account.States;
using EasyRent.Identity.Shared.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Identity.Infrastructure.Registration;

public static class JwtAuthorizationRegistration
{
    public static IServiceCollection AddJwtBearerAuthorization(this IServiceCollection services)
        => services.AddAuthorization(opt =>
        {
            opt.AddPolicy(nameof(AuthorizationPolicies.ModeratorPolicy),
                policy => policy.RequireRole(AuthorizationPolicies.ModeratorPolicy.Role));
            opt.AddPolicy(nameof(AuthorizationPolicies.TenantPolicy),
                policy => policy.RequireRole(AuthorizationPolicies.TenantPolicy.Role));
            opt.AddPolicy(nameof(AuthorizationPolicies.PlaceOwnerPolicy),
                policy => policy.RequireRole(AuthorizationPolicies.PlaceOwnerPolicy.Role));
            opt.AddPolicy(nameof(AuthorizationPolicies.SuspendPolicy),
                policy => policy.RequireClaim(AuthorizationPolicies.SuspendPolicy.Claim, AccountState.Active.ToString()));
        });
}