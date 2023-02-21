using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.RentalAd.ReadModels.Dtos;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceOwnerCreatedProjection : DefaultProjection<RentalAdPlaceOwnerCreated>
{
    public RentalAdPlaceOwnerCreatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceOwnerCreated @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        rentalAd.PlaceOwner = new PlaceOwnerDto
        {
            PlaceOwnerId = @event.PlaceOwnerId,
            EmailAddress = @event.EmailAddress,
        };

        await documentRepository.ReplaceAsync(rentalAd);

        Logger.Trace("> Rental Ad Place Owner with ID: #{RentalAd} stored in the document store", @event.RentalAdId);
    }
}