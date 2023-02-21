using Microsoft.Extensions.Options;

namespace EasyRent.Configuration;

public class ModuleConfigurationProvider<TConfiguration> : IConfigurationProvider<TConfiguration>
    where TConfiguration : class
{
    private readonly IOptionsMonitor<TConfiguration> _configurationOptions;

    public ModuleConfigurationProvider(IOptionsMonitor<TConfiguration> configurationOptions)
        => _configurationOptions = configurationOptions;

    public TConfiguration GetConfiguration()
        => _configurationOptions.CurrentValue;
}