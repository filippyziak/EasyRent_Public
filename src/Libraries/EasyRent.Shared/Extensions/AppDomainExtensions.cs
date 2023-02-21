using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRent.Shared.Extensions;

public static class AppDomainExtensions
{
    public static IReadOnlyList<Type> GetApplicationTypes()
        => AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .ToList();
}