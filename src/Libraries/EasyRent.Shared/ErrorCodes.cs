namespace EasyRent.Shared;

public static class ErrorCodes
{
    public const string UnhandledException = nameof(UnhandledException);
    public const string InvalidEntityState = nameof(InvalidEntityState);
    public const string ValidationFailed = nameof(ValidationFailed);
    public const string DomainOperationNotRegistered = nameof(DomainOperationNotRegistered);
    public const string EntityAlreadyExists = nameof(EntityAlreadyExists);
    public const string ResourceExpired = nameof(ResourceExpired);
    public const string FileStorageOperationFailed = nameof(FileStorageOperationFailed);
    public const string PermissionFailed = nameof(PermissionFailed);
}