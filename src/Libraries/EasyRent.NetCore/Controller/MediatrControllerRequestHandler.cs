using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EasyRent.CQRS.Extensions;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Shared.Extensions;
using EasyRent.Shared.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasyRent.NetCore.Controller;

public class MediatrControllerRequestHandler
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    private static readonly IReadOnlyList<Type> _applicationAssembliesTypes = AppDomainExtensions.GetApplicationTypes();

    public MediatrControllerRequestHandler(ILogger logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> HandleRequestAsync<TRequest, TResult>(ControllerBase controller, TRequest request)
        where TRequest : IRequest<TResult>
        where TResult : BaseResponse
    {
        _logger.Info("Request {Request} sent", request.GetType().Name);

        var validatorType = _applicationAssembliesTypes
            .FirstOrDefault(t => t.GetInterfaces().Any(i => i.IsGenericType
                                                            && i.GenericTypeArguments[0] == typeof(TRequest))
                                 && t.BaseType == typeof(AbstractValidator<TRequest>));

        if (validatorType is not null)
        {
            var validator = Activator.CreateInstance(validatorType) as IValidator<TRequest>;

            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var validationFailures = validationResult.Errors.ToValidationFailures();
                    var validationFailureResult = BaseResponse.ValidationFailure(validationFailures);

                    _logger.Warning("Request validation failed. Validation failures:\n{ValidationFailures}",
                        JsonConvert.SerializeObject(validationFailures, new JsonSerializerSettings { Formatting = Formatting.Indented }));

                    return CreateResponse(controller, validationFailureResult);
                }
            }
        }

        var result = await _mediator.Send(request);

        _logger.Info("Request {Request} completed", request.GetType().Name);

        return CreateResponse(controller, result);
    }

    private static IActionResult CreateResponse(ControllerBase controller, BaseResponse response)
        => response.IsSucceeded
            ? controller.Ok(response)
            : controller.StatusCode((int)HttpStatusCode.InternalServerError, response);
}