using System;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.NetCore.HttpContext;
using EasyRent.Shared;
using EasyRent.Shared.Exceptions;
using EasyRent.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EasyRent.NetCore.Middlewares;

public class UnhandledExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public UnhandledExceptionMiddleware(RequestDelegate next)
        => _next = next;

    public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger>();

        try
        {
            await _next(context);
        }
        catch (BaseException be)
        {
            await WriteErrorAsync(logger, context, be, errorCode: be.ErrorCode);
        }
        catch (Exception e)
        {
            await WriteErrorAsync(logger, context, e);
        }
    }

    private Task WriteErrorAsync(ILogger logger,
        Microsoft.AspNetCore.Http.HttpContext context,
        Exception exception,
        string errorCode = ErrorCodes.UnhandledException)
    {
        logger.Error(exception.Message, exception);

        var invalidResult = new BaseResponse(new Error(
            errorCode,
            exception.Message));

        context.Response.AddApplicationError(errorCode);

        return context.Response.WriteAsync(JsonConvert.SerializeObject(invalidResult, Formatting.Indented));
    }
}