using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EasyRent.Infrastructure.Abstractions.DocumentStore;

public interface IDocumentRepository<TDocument>
    where TDocument : BaseDocument, new()
{
    Task<TDocument> FindAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TDocument>> ScanAsync(CancellationToken cancellationToken = default);
    Task StoreAsync(TDocument document, CancellationToken cancellationToken = default);
    Task ReplaceAsync(TDocument document, CancellationToken cancellationToken = default);
    Task DeleteAsync(TDocument document, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default);
}