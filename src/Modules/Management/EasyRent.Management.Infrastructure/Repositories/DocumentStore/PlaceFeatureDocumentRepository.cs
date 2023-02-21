using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.DocumentStore;
using EasyRent.Management.Infrastructure.DocumentStore.Documents;
using EasyRent.Management.Infrastructure.Repositories.DocumentStore.Abstractions;
using MongoDB.Driver;

namespace EasyRent.Management.Infrastructure.Repositories.DocumentStore;

public class PlaceFeatureDocumentRepository : MongoDbDocumentRepository<PlaceFeatureDocument>, IPlaceFeatureDocumentRepository
{
    public PlaceFeatureDocumentRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
    {
    }

    public async Task<IReadOnlyList<PlaceFeatureDocument>> GetPlaceFeatures(CancellationToken cancellationToken)
        => await Collection.AsQueryable().ToListAsync();
}