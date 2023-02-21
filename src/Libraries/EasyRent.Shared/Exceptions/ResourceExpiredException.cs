namespace EasyRent.Shared.Exceptions;

public class ResourceExpiredException : BaseException
{
    public override string ErrorCode => ErrorCodes.ResourceExpired;

    public ResourceExpiredException(object resource)
        : base($"Resource type'{resource.GetType()}' is already expired")
    {
    }
}