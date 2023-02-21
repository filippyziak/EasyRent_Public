using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdMainPictureRemovedProjection : DefaultProjection<RentalAdMainPictureRemoved>
{
    public RentalAdMainPictureRemovedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdMainPictureRemoved @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        rentalAd.MainPlacePicture = null;

        await documentRepository.ReplaceAsync(rentalAd);
        
        Logger.Trace("> Main picture removed from rental ad with ID: #{RentalAdId}", rentalAd.RentalAdId);
    }
}