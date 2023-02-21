using System;

namespace EasyRent.Shared.Exceptions;

public abstract class BaseException : Exception
{
    public virtual string ErrorCode => ErrorCodes.UnhandledException;

    protected BaseException(string message) : base(message)
    {
    }
}