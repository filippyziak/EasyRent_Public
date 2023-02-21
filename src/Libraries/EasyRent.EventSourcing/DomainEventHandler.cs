namespace EasyRent.EventSourcing;

public abstract class DomainEventHandler<TEntity, TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TEntity : IInternalEventHandler
    where TDomainEvent : IDomainEvent
{
    protected readonly TEntity Entity;

    protected DomainEventHandler(TEntity entity) => Entity = entity;

    public abstract void Handle(TDomainEvent domainEvent);
}