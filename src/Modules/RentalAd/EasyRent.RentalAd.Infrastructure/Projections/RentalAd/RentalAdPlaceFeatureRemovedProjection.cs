using System.Linq;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceFeatureRemovedProjection : DefaultProjection<RentalAdPlaceFeatureRemoved>
{
    public RentalAdPlaceFeatureRemovedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceFeatureRemoved @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        rentalAd.PlaceFeatures.Remove(rentalAd.PlaceFeatures.FirstOrDefault(f
            => f.PlaceFeatureId == @event.PlaceFeatureId));

        await documentRepository.ReplaceAsync(rentalAd);

        Logger.Trace("> Place feature removed from rental ad with ID: #{RentalAdId} and has been updated in document store.", rentalAd.RentalAdId);
    }
}