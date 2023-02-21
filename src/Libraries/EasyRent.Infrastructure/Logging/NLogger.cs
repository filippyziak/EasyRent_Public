using System;
using NLog;

namespace EasyRent.Infrastructure.Logging;

public class NLogger : Abstractions.Abstractions.ILogger
{
    private readonly Logger _logger;

    public NLogger(Logger logger) => _logger = logger;

    public void Trace(string message, params object[] args)
        => _logger.Trace(message, args);

    public void Info(string message, params object[] args)
        => _logger.Info(message, args);

    public void Warning(string message, params object[] args)
        => _logger.Warn(message, args);

    public void Error(string message, params object[] args)
        => _logger.Error(message, args);

    public void Error(string message, Exception e, params object[] args)
        => _logger.Error(e, message, args);
}