using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EasyRent.Infrastructure.DocumentStore;

public class MongoDbDocumentRepository<TDocument> : IDocumentRepository<TDocument>
    where TDocument : BaseDocument, new()
{
    protected readonly IMongoCollection<TDocument> Collection;

    public MongoDbDocumentRepository(string connectionString, string databaseName)
    {
        var database = new MongoClient(connectionString).GetDatabase(databaseName);
        Collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    public Task<TDocument> FindAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken)
        => Collection
            .Find(predicate)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<TDocument>> ScanAsync(CancellationToken cancellationToken)
        => await Collection
            .Find(x => true)
            .ToListAsync(cancellationToken);

    public Task StoreAsync(TDocument document, CancellationToken cancellationToken)
        => Collection.InsertOneAsync(document, cancellationToken: cancellationToken);

    public Task ReplaceAsync(TDocument document, CancellationToken cancellationToken)
    {
        document.ModifiedOn = DateTime.UtcNow;
        return Collection.ReplaceOneAsync(filter: x => x.Id == document.Id, document, cancellationToken: cancellationToken);
    }

    public Task DeleteAsync(TDocument document, CancellationToken cancellationToken)
        => Collection.DeleteOneAsync(filter: x => x.Id == document.Id, cancellationToken);

    public Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> predicate, CancellationToken cancellationToken = default)
        => Collection.AsQueryable().AnyAsync(predicate, cancellationToken);

    private protected string GetCollectionName(Type documentType)
        => ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
}