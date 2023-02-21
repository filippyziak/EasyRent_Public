using System;

namespace EasyRent.Shared.Exceptions;

public class EntityNotFoundException : BaseException
{
    public EntityNotFoundException(Guid entityId, Type entityType)
        : base($"Entity of type: '{entityType.Name}' with ID: '{entityId}' was not found")
    {
    }

    public EntityNotFoundException(Type entityType)
        : base($"Entity of type: '{entityType.Name}' was not found")
    {
    }
}