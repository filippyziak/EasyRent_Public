using System;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Shared;
using EasyRent.Shared.Exceptions;
using EasyRent.Shared.Models;
using MediatR;

namespace EasyRent.CQRS.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BaseResponse
{
    private readonly ILogger _logger;

    public UnhandledExceptionBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (BaseException e)
        {
            return HandleException(e, e.ErrorCode);
        }
        catch (Exception e)
        {
            return HandleException(e, ErrorCodes.UnhandledException);
        }
    }

    private TResponse HandleException(Exception e, string errorCode)
    {
        var requestName = typeof(TRequest).Name;
        _logger.Error("{RequestName} | {Message}", e, requestName, e.Message);

        var invalidResponse = Activator.CreateInstance(typeof(TResponse),
            new Error(errorCode, e.Message));

        return (TResponse)invalidResponse;
    }
}