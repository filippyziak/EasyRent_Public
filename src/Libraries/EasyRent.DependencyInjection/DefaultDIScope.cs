using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace EasyRent.DependencyInjection;

internal class DefaultDIScope : IDIScope
{
    private readonly IServiceScope _scope;

    public DefaultDIScope(IServiceScope scope) => _scope = scope;

    public TService ResolveService<TService>()
        => _scope.ServiceProvider.GetRequiredService<TService>();

    public IReadOnlyList<TService> ResolveServices<TService>()
        => _scope.ServiceProvider.GetServices<TService>().ToList();

    public void Dispose() => _scope.Dispose();
}