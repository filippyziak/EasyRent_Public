using NLog;
using NLog.Web;
using ILogger = EasyRent.Infrastructure.Abstractions.Abstractions.ILogger;

namespace EasyRent.Infrastructure.Logging;

public class NLogFactory
{
    public static ILogger GetLoggerFromConfiguration()
        => new NLogger(LogManager.Setup()
            .LoadConfigurationFromAppSettings()
            .GetCurrentClassLogger());
}