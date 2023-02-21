using System.Linq;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceFeaturePictureUrlUpdatedProjection : DefaultProjection<RentalAdPlaceFeaturePictureUrlUpdated>
{
    public RentalAdPlaceFeaturePictureUrlUpdatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceFeaturePictureUrlUpdated @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        var placeFeature = rentalAd.PlaceFeatures
            .FirstOrDefault(f => f.PlaceFeatureId == @event.PlaceFeatureId);

        if (placeFeature is not null)
        {
            rentalAd.PlaceFeatures.Remove(placeFeature);
            rentalAd.PlaceFeatures.Add(placeFeature with
            {
                PictureUrl = @event.NewPictureUrl
            });

            await documentRepository.ReplaceAsync(rentalAd);

            Logger.Trace("> Place feature pictureUrl for rental ad with ID: #{RentalAdId} and has been updated in document store.", rentalAd.RentalAdId);
        }
    }
}