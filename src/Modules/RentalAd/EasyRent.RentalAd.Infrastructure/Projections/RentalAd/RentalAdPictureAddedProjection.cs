using System.Linq;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.States;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.RentalAd.ReadModels.Dtos;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPictureAddedProjection : DefaultProjection<RentalAdPictureAdded>
{
    public RentalAdPictureAddedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPictureAdded @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        if (!rentalAd.PlacePictures.Any(p => p.PlacePictureId == @event.PlacePictureId))
        {
            rentalAd.PlacePictures.Add(new PlacePictureDto
            {
                PlacePictureId = @event.PlacePictureId,
                PictureUrl = @event.PlacePictureUrl,
                PlacePictureState = PlacePictureState.PlacePicture.ToString()
            });

            await documentRepository.ReplaceAsync(rentalAd);

            Logger.Trace("> Picture added to rental ad with ID: #{RentalAdId} and has been updated in document store.", rentalAd.RentalAdId);
        }
    }
}