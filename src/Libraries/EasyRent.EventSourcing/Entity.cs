using System;

namespace EasyRent.EventSourcing;

public abstract class Entity<TId> : IInternalEventHandler
    where TId : EntityId
{
    readonly Action<IDomainEvent> _applier;

    public Entity(Action<IDomainEvent> applier) => _applier = applier;

    public void Apply(IDomainEvent domainEvent)
    {
        When(domainEvent);
        _applier(domainEvent);
    }

    private void When(IDomainEvent domainEvent)
        => DomainEventHandlerExecutor.Handle(this, domainEvent);

    void IInternalEventHandler.Handle(IDomainEvent domainEvent) => When(domainEvent);
}