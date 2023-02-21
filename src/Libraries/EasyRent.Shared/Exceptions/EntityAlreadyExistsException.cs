using System;

namespace EasyRent.Shared.Exceptions;

public class EntityAlreadyExistsException : BaseException
{
    public override string ErrorCode => ErrorCodes.EntityAlreadyExists;

    public EntityAlreadyExistsException(Guid entityId, Type entityType)
        : base($"Entity of type: '{entityType.Name}' with Id: '{entityId}' already exists")
    {
    }
    
    public EntityAlreadyExistsException( Type entityType)
        : base($"Entity of type: '{entityType.Name}' already exists")
    {
    }
}