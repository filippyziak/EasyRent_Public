using System;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.RentalAd.ReadModels.Dtos;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceAddressCityUpdatedProjection : DefaultProjection<RentalAdPlaceAddressCityUpdated>
{
    public RentalAdPlaceAddressCityUpdatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceAddressCityUpdated @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        rentalAd.PlaceAddress = rentalAd.PlaceAddress with
        {
            City = @event.NewCity,
            Street = @event.NewStreet
        };

        await documentRepository.ReplaceAsync(rentalAd);

        Logger.Trace("Place address ID: #{PlaceAddressId} has been added to database", @event.PlaceAddressId);
    }
}