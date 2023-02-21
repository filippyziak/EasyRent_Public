using System.Threading.Tasks;

namespace EasyRent.EventSourcing;

public interface IAggregateRepository
{
    Task<T> LoadAsync<T, TId>(TId aggregateId)
        where T : AggregateRoot<TId>
        where TId : AggregateId;

    Task SaveAsync<T, TId>(T aggregate)
        where T : AggregateRoot<TId>
        where TId : AggregateId;

    Task<bool> ExistsAsync<T, TId>(TId aggregateId)
        where T : AggregateRoot<TId>
        where TId : AggregateId;
}