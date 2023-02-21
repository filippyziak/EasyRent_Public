using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasyRent.Identity.Core.Abstractions;
using EasyRent.Identity.Infrastructure.Configurations;
using EasyRent.Identity.ReadModels.ReadModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EasyRent.Identity.Infrastructure.Services;

public class JwtAuthTokenProvider : IAuthTokenProvider
{
    private readonly IOptionsMonitor<IdentityTokenConfiguration> _identityTokenConfigurationOptions;

    public JwtAuthTokenProvider(IOptionsMonitor<IdentityTokenConfiguration> identityTokenConfigurationOptions)
    {
        _identityTokenConfigurationOptions = identityTokenConfigurationOptions;
    }

    public string CreateToken(AccountReadModel userAccount, CancellationToken cancellationToken = default)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userAccount.AccountId.ToString()),
            new Claim(ClaimTypes.Email, userAccount.EmailAddress),
            new Claim(ClaimTypes.Role, userAccount.AccountType),
            new Claim("State", userAccount.State)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityTokenConfigurationOptions.CurrentValue.SecretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriber = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_identityTokenConfigurationOptions.CurrentValue.TokenExpirationTimeInMinutes),
            SigningCredentials = credentials
        };

        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var token = jwtTokenHandler.CreateToken(tokenDescriber);

        return jwtTokenHandler.WriteToken(token);
    }
}