using System;

namespace EasyRent.Infrastructure.Abstractions.Abstractions;

public interface ILogger
{
    void Trace(string message, params object[] args);
    void Info(string message, params object[] args);
    void Warning(string message, params object[] args);
    void Error(string message, params object[] args);
    void Error(string message, Exception exception, params object[] args);
}