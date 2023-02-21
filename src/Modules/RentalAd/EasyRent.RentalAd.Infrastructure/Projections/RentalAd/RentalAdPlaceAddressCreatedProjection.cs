using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.RentalAd.ReadModels.Dtos;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceAddressCreatedProjection : DefaultProjection<RentalAdPlaceAddressCreated>
{
    public RentalAdPlaceAddressCreatedProjection(ILogger logger, IDIProvider diProvider
    ) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceAddressCreated @event)
    {
        var rentalAdDocumentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await rentalAdDocumentRepository.FindAsync(x 
            => x.RentalAdId == @event.RentalAdId.ToString());
        
        if (rentalAd is not null)
        {
            rentalAd.PlaceAddress = new PlaceAddressDto
            {
                PlaceAddressId = @event.PlaceAddressId,
                Country = @event.Country,
                City = @event.City,
                Street = @event.Street
            };
            
        }

        Logger.Trace("> Place address ID: #{PlaceAddressId} has been added to database", @event.PlaceAddressId);
    }
}