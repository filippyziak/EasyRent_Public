using System.Linq;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.States;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdMainPictureSetProjection : DefaultProjection<RentalAdMainPictureSet>
{
    public RentalAdMainPictureSetProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdMainPictureSet @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        var mainPicture = rentalAd.PlacePictures.First(p
            => p.PlacePictureId == @event.PlacePictureId);

        rentalAd.MainPlacePicture = mainPicture with
        {
            PlacePictureState = PlacePictureState.MainPlacePicture.ToString()
        };

        rentalAd.MainPlacePicture = mainPicture;
        await documentRepository.ReplaceAsync(rentalAd);

        Logger.Trace("> Picture ha been set to main to rental ad with ID: #{RentalAdId} and has been updated in document store.", rentalAd.RentalAdId);
    }
}