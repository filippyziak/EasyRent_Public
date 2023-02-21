using System;

namespace EasyRent.Shared.Exceptions;

public class PermissionException : BaseException
{
    public override string ErrorCode => ErrorCodes.PermissionFailed;

    public PermissionException(Type resourceType) : base($"You are not allowed to this resource {resourceType}")
    {
    }
    
    public PermissionException(string message) : base(message)
    {
    }
}