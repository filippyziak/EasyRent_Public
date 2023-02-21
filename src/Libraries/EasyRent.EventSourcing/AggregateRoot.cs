using System.Collections.Generic;
using System.Linq;

namespace EasyRent.EventSourcing;

public abstract class AggregateRoot<TId> : IInternalEventHandler
    where TId : AggregateId
{
    public int Version { get; private set; } = -1;

    private readonly ICollection<IDomainEvent> _domainEventsHistory = new List<IDomainEvent>();

    public void Load(IEnumerable<IDomainEvent> domainEventsHistory)
    {
        foreach (var domianEvent in domainEventsHistory)
        {
            When(domianEvent);
            Version++;
        }
    }

    public IReadOnlyList<IDomainEvent> GetDomainEventsHistory() => _domainEventsHistory.ToList();

    public void ClearDomainEventsHistory() => _domainEventsHistory.Clear();

    protected abstract void EnsureValidState();

    public void Apply(IDomainEvent domainEvent)
    {
        When(domainEvent);
        EnsureValidState();
        _domainEventsHistory.Add(domainEvent);
    }

    public void ApplyToEntity(IInternalEventHandler entity, IDomainEvent domainEvent) => entity?.Handle(domainEvent);

    private void When(IDomainEvent domainEvent) => DomainEventHandlerExecutor.Handle(this, domainEvent);

    void IInternalEventHandler.Handle(IDomainEvent domainEvent) => When(domainEvent);
}