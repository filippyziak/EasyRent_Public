using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Management.Domain.PlaceFeatures.DomainEvents.V1;
using EasyRent.Management.Infrastructure.DocumentStore.Documents;
using EasyRent.Management.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.Management.Infrastructure.Projections.PlaceFeatures.V1;

public class PlaceFeatureCreatedProjection : DefaultProjection<PlaceFeatureCreated>
{
    public PlaceFeatureCreatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(PlaceFeatureCreated @event)
    {
        var documentRepository = Scope.ResolveService<IPlaceFeatureDocumentRepository>();

        if (!await documentRepository.ExistsAsync(a => a.PlaceFeatureId == @event.PlaceFeatureId))
        {
            await documentRepository.StoreAsync(new PlaceFeatureDocument()
            {
                PlaceFeatureId = @event.PlaceFeatureId,
                Description = @event.Description,
                PictureUrl = @event.PictureUrl
            });

            Logger.Trace("> Place feature with ID: #{PlaceFeatureId} stored in the document store", @event.PlaceFeatureId);
        }
    }
}