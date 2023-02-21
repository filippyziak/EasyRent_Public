using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRent.Infrastructure.Abstractions.Database;

public interface IPersistenceRepository<TModel, TKey>
    where TModel : BasePersistenceModel
{
    Task<TModel> FindAsync(TKey key, CancellationToken cancellationToken = default);
    Task<TModel> FindAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default);

    void Add(TModel entity);
    void Update(TModel entity);
    void Delete(TModel entity);
}