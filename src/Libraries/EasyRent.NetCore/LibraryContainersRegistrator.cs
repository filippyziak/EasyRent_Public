using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyRent.Configuration;
using EasyRent.NetCore.Controller;
using EasyRent.NetCore.Cors;
using EasyRent.NetCore.Filters;
using EasyRent.NetCore.HttpContext;
using EasyRent.NetCore.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EasyRent.NetCore;

public static class LibraryContainersRegistrator
{
    public static IServiceCollection AddAspNetCore(this IServiceCollection services,
        string serviceName,
        string version,
        IConfiguration configuration,
        IEnumerable<string> assembliesNames,
        IEnvironmentProvider environmentProvider)
    {
        services
            .AddControllers()
            .AddApplicationParts(assembliesNames)
            .AddControllersAsServices()
            .AddMvcOptions(options => options.Filters.Add<ValidationFilter>())
            .AddNewtonsoftJsonSerializer()
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        services
            .AddHttpContextAccessor()
            .AddSingleton<IReadOnlyHttpAccessor, HttpAccessor>();

        services.AddCorsFromConfiguration(configuration);

        services.AddScoped<MediatrControllerRequestHandler>();

        services
            .AddSwaggerWithAuthentication(serviceName, version)
            .AddEndpointsApiExplorer();

        services.AddSignalR();

        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        return services;
    }

    private static IMvcBuilder AddApplicationParts(this IMvcBuilder mvcBuilder, IEnumerable<string> assembliesNames)
    {
        assembliesNames.ToList()
            .ForEach(assemblyName => mvcBuilder.AddApplicationPart(Assembly.Load(assemblyName)));

        return mvcBuilder;
    }

    public static IApplicationBuilder UseAspNetCore(this WebApplication app, IEnvironmentProvider environmentProvider)
    {
        if (!environmentProvider.IsProd)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        app.UseHttpsRedirection();

        app
            .UseMiddleware<UnhandledExceptionMiddleware>()
            .UseMiddleware<LoggingRequestScopeMiddleware>();

        return app;
    }

    private static IMvcBuilder AddNewtonsoftJsonSerializer(this IMvcBuilder services)
        => services.AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
}