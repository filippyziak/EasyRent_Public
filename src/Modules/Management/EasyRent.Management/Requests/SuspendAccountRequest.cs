using System;

namespace EasyRent.Management.Requests;

public record SuspendAccountRequest(Guid AccountId);