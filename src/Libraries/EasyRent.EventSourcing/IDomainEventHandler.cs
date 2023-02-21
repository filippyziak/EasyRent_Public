namespace EasyRent.EventSourcing;

public interface IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    void Handle(TDomainEvent domainEvent);
}