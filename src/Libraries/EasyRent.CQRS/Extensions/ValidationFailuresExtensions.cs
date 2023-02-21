using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace EasyRent.CQRS.Extensions;

public static class ValidationFailuresExtensions
{
    public static IDictionary<string, IEnumerable<string>> ToValidationFailures(this IEnumerable<ValidationFailure> failures)
        => failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(group => group.Key, group => group.AsEnumerable());
}