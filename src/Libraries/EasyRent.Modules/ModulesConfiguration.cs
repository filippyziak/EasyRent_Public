using System.Collections.Generic;
using System.Linq;

namespace EasyRent.Modules;

public class ModulesConfiguration
{
    private const string ProjectName = "EasyRent";

    private readonly ModuleManifest[] _modules;

    public ModulesConfiguration(ModuleManifest[] modules)
        => _modules = modules;

    public HashSet<string> EnabledModules { get; init; } = new();

    public HashSet<string> EnabledModulesAssembliesNames
        => EnabledModules
            .Select(moduleName => $"{ProjectName}.{moduleName}")
            .ToHashSet();

    public IReadOnlyList<ModuleManifest> GetEnabledModules()
        => _modules
            .Where(module => EnabledModules.Contains(module.Name))
            .ToList();
}