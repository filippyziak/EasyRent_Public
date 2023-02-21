namespace EasyRent.Shared.Exceptions;

public class InvalidEntityStateException : BaseException
{
    public override string ErrorCode => ErrorCodes.InvalidEntityState;

    public InvalidEntityStateException(object entity, string message)
        : base($"Entity '{entity.GetType().Name}' state is invalid. {message}")
    {
    }
}