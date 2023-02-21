namespace EasyRent.EventSourcing;

public interface IInternalEventHandler
{
    void Handle(IDomainEvent domainEvent);
}