using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceOwnerEmailAddressUpdatedProjection : DefaultProjection<RentalAdPlaceOwnerEmailAddressUpdated
>
{
    public RentalAdPlaceOwnerEmailAddressUpdatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceOwnerEmailAddressUpdated @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        if (rentalAd is not null)
        {
            rentalAd.PlaceOwner = rentalAd.PlaceOwner with
            {
                EmailAddress = @event.NewEmailAddress
            };

            await documentRepository.ReplaceAsync(rentalAd);

            Logger.Trace("> Rental Ad with ID: #{RentalAdId} Place Owner Email Address has been updated in document store.", rentalAd.RentalAdId);
        }
    }
}