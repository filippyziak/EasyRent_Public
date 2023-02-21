using System;
using System.Collections.Generic;

namespace EasyRent.DependencyInjection;

public interface IDIScope : IDisposable
{
    TService ResolveService<TService>();
    IReadOnlyList<TService> ResolveServices<TService>();
}