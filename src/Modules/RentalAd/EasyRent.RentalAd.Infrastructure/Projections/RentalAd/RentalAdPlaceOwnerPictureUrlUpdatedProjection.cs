using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceOwnerPictureUrlUpdatedProjection : DefaultProjection<RentalAdPlaceOwnerPictureUrlUpdated>
{
    public RentalAdPlaceOwnerPictureUrlUpdatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceOwnerPictureUrlUpdated @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        if (rentalAd is not null)
        {
            rentalAd.PlaceOwner = rentalAd.PlaceOwner with
            {
                PictureUrl = @event.NewPictureUrl
            };

            await documentRepository.ReplaceAsync(rentalAd);

            Logger.Trace("> Rental Ad with ID: #{RentalAdId} Place Owner Email Address has been updated in document store.", rentalAd.RentalAdId);
        }
    }
}