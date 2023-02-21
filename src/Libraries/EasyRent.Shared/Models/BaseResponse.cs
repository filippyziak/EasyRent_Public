using System.Collections.Generic;
using EasyRent.Shared.Exceptions;

namespace EasyRent.Shared.Models;

public record BaseResponse(Error Error = null)
{
    public IDictionary<string, IEnumerable<string>> ValidationFailures { get; init; }
    public bool IsSucceeded => Error is null;

    public static BaseResponse ValidationFailure(IDictionary<string, IEnumerable<string>> validationFailures)
        => new(new Error(
            ErrorCodes.ValidationFailed,
            ValidationException.CustomMessage,
            validationFailures));
}