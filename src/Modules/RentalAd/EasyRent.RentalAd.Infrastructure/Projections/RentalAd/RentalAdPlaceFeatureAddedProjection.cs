using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.RentalAd.ReadModels.Dtos;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceFeatureAddedProjection : DefaultProjection<RentalAdPlaceFeatureAdded>
{
    public RentalAdPlaceFeatureAddedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceFeatureAdded @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        rentalAd.PlaceFeatures.Add(new PlaceFeatureDto
        {
            PlaceFeatureId = @event.PlaceFeatureId,
            Description = @event.Description,
            PictureUrl = @event.PictureUrl
        });

        await documentRepository.ReplaceAsync(rentalAd);

        Logger.Trace("> Place feature added to rental ad with ID: #{RentalAdId} and has been updated in document store.", rentalAd.RentalAdId);
    }
}