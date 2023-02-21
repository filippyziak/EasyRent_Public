using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Core.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.States;
using EasyRent.RentalAd.Infrastructure.DocumentStore.Documents;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;
using EasyRent.RentalAd.ReadModels.Dtos;
using EasyRent.Shared.Exceptions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdCreatedProjection : DefaultProjection<RentalAdCreated>
{
    public RentalAdCreatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdCreated @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();
        var placeOwnerReadOnlyRepository = Scope.ResolveService<IPlaceOwnerReadOnlyRepository>();

        var placeOwner = await placeOwnerReadOnlyRepository.GetPlaceOwnerByIdAsync(@event.PlaceOwnerId)
                         ?? throw new EntityNotFoundException(@event.PlaceOwnerId, typeof(PlaceOwner));

        if (!await documentRepository.ExistsAsync(r => r.RentalAdId == @event.RentalAdId.ToString()))
        {
            await documentRepository.StoreAsync(new RentalAdDocument
            {
                RentalAdId = @event.RentalAdId.ToString(),
                PlaceAddressId = @event.PlaceAddressId,
                Title = @event.Title,
                Description = @event.Description,
                PricePerDay = @event.PricePerDay,
                State = RentalAdState.Active.ToString(),
                PlaceOwner = new PlaceOwnerDto
                {
                    PlaceOwnerId = placeOwner.PlaceOwnerId,
                    EmailAddress = placeOwner.EmailAddress
                },
                PlaceAddress = new PlaceAddressDto
                {
                    Country = @event.Country,
                    City = @event.City,
                    Street = @event.Street
                }
            });

            Logger.Trace("> Rental Ad with ID: #{RentalAd} stored in the document store", @event.RentalAdId);
        }
    }
}