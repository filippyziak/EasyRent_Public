using System.Collections.Generic;
using System.IO;
using EasyRent.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EasyRent.Modules;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureModules(this IHostBuilder hostBuilder,
        IEnvironmentProvider environmentProvider)
        => hostBuilder.ConfigureAppConfiguration((context, configuration) =>
        {
            IEnumerable<string> GetSettings(string pattern)
                => Directory.EnumerateFiles(context.HostingEnvironment.ContentRootPath,
                    $"module.{pattern}.json", SearchOption.AllDirectories);

            foreach (var settings in GetSettings("*"))
                configuration.AddJsonFile(settings, optional: true, reloadOnChange: true);

            foreach (var settings in GetSettings($"*.{environmentProvider.Stage}"))
                configuration.AddJsonFile(settings, optional: true, reloadOnChange: true);
        });
}