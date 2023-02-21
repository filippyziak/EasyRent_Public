using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EasyRent.NetCore;

public static class SwaggerRegistrator
{
    private const string DefaultVersion = "v1";
    private const string Bearer = nameof(Bearer);

    public static IServiceCollection AddSwaggerWithAuthentication(this IServiceCollection services, string serviceName, string version = DefaultVersion)
        => services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(version, new OpenApiInfo { Title = serviceName, Version = version });
            options.AddSecurityDefinition(Bearer, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = Bearer
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = Bearer
                        }
                    },
                    new string[] { }
                }
            });
        });
}