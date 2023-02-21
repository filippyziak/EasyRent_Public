using System.Collections.Generic;

namespace EasyRent.Shared.Models;

public record Error
(
    string ErrorCode,
    string Message,
    IDictionary<string, IEnumerable<string>> validationFailures = null
);