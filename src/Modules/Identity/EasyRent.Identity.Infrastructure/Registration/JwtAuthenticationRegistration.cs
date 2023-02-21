using System.Text;
using EasyRent.Identity.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EasyRent.Identity.Infrastructure.Registration;

public static class JwtAuthenticationRegistration
{
    private const int DefaultClockSkewInMinutes = 1;

    public static AuthenticationBuilder AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(
                    configuration.GetSection(nameof(IdentityTokenConfiguration))
                        .Get<IdentityTokenConfiguration>()
                        .SecretKey);

                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(DefaultClockSkewInMinutes)
                };
            });
}