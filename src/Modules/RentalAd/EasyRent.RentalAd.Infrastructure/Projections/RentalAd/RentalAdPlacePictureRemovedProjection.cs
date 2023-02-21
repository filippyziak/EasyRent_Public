using System.Linq;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlacePictureRemovedProjection : DefaultProjection<RentalAdPlacePictureRemoved>
{
    public RentalAdPlacePictureRemovedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlacePictureRemoved @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        var pictureToRemove = rentalAd.PlacePictures.FirstOrDefault(p => p.PlacePictureId == @event.PictureId);
        
        rentalAd.PlacePictures.Remove(pictureToRemove);

        await documentRepository.ReplaceAsync(rentalAd);
        
        Logger.Trace("> Picture removed from rental ad with ID: #{RentalAdId}", rentalAd.RentalAdId);

    }
}