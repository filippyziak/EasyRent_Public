using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;
using NLog;

namespace EasyRent.NetCore.Middlewares;

public class LoggingRequestScopeMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingRequestScopeMiddleware(RequestDelegate next)
        => _next = next;

    public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
    {
        using (ScopeContext.PushProperty(LoggingScope.Request.ScopeName,
                   LoggingScope.Request.ParseScopeMessage(context.TraceIdentifier,
                       context.Request.Method,
                       context.Request.Path)))
        {
            await _next(context);
        }
    }
}