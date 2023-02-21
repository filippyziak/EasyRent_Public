using System.Collections.Generic;

namespace EasyRent.Shared.Exceptions;

public class ValidationException : BaseException
{
    public const string CustomMessage = "One or more validation failures have occurred";

    public IDictionary<string, IEnumerable<string>> ValidationFailures { get; }

    public override string ErrorCode => ErrorCodes.ValidationFailed;

    public ValidationException(string message = CustomMessage) : base(message)
    {
    }

    public ValidationException(IDictionary<string, IEnumerable<string>> validationFailures) : this()
        => ValidationFailures = validationFailures;
}