using EasyRent.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.Modules;

public abstract class ModuleManifest
{
    public abstract string Name { get; }

    public virtual void Register(IServiceCollection services, IConfiguration configuration, IEnvironmentProvider environmentProvider)
    {
    }

    public virtual void Use(WebApplication app, IEnvironmentProvider environmentProvider)
    {
    }
}