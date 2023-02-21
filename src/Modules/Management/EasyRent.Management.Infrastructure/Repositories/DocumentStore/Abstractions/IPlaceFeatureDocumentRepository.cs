using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Management.Infrastructure.DocumentStore.Documents;

namespace EasyRent.Management.Infrastructure.Repositories.DocumentStore.Abstractions;

public interface IPlaceFeatureDocumentRepository : IDocumentRepository<PlaceFeatureDocument>
{
    Task<IReadOnlyList<PlaceFeatureDocument>> GetPlaceFeatures(CancellationToken cancellationToken);
}