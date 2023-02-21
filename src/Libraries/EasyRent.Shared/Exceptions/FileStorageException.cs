namespace EasyRent.Shared.Exceptions;

public class FileStorageException : BaseException
{
    public override string ErrorCode => ErrorCodes.FileStorageOperationFailed;

    public FileStorageException(string message) : base(message)
    {
    }
}