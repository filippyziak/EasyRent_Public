using System;
using System.Linq;
using EasyRent.Shared.Exceptions;
using EasyRent.Shared.Extensions;

namespace EasyRent.EventSourcing;

public class DomainEventHandlerExecutor
{
    public static void Handle(IInternalEventHandler entity, IDomainEvent domainEvent)
    {
        var domainEventType = domainEvent.GetType();
        var domainEventHandlerType = AppDomainExtensions.GetApplicationTypes()
                                         .FirstOrDefault(t
                                             => t.IsClass
                                                && t.GetInterfaces()
                                                    .Any(interfaceType
                                                        => interfaceType.IsGenericType
                                                           && interfaceType.GenericTypeArguments[0] == domainEventType)
                                                && t.BaseType.IsGenericType
                                                && t.BaseType.GenericTypeArguments[0] == entity.GetType())
                                     ?? throw new DomainEventHandlerNotRegisteredException(domainEventType);

        var domainEventHandler = Activator.CreateInstance(domainEventHandlerType,
            args: new[] { entity });

        domainEventHandlerType
            .GetMethod("Handle")
            .Invoke(domainEventHandler, parameters: new[] { domainEvent });
    }
}