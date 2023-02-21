using System.Linq;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceReservationRemovedProjection : DefaultProjection<RentalAdPlaceReservationRemoved>
{
    public RentalAdPlaceReservationRemovedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceReservationRemoved @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        rentalAd.PlaceReservations.Remove(rentalAd.PlaceReservations.FirstOrDefault(f
            => f.PlaceReservationId == @event.PlaceReservationId));

        await documentRepository.ReplaceAsync(rentalAd);

        Logger.Trace("> Place reservation removed from rental ad with ID: #{RentalAdId} and has been updated in document store.", rentalAd.RentalAdId);
    }
}