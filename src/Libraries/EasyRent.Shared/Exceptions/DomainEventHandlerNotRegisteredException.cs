using System;

namespace EasyRent.Shared.Exceptions;

public class DomainEventHandlerNotRegisteredException : BaseException
{
    public override string ErrorCode => ErrorCodes.DomainOperationNotRegistered;

    public DomainEventHandlerNotRegisteredException(Type domainEventType)
        : base($"Domain event handler for type '{domainEventType.Name}' is not registered")
    {
    }
}