using System.Linq;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceReservationArchivedProjection : DefaultProjection<RentalAdPlaceReservationArchived>
{
    public RentalAdPlaceReservationArchivedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceReservationArchived @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        rentalAd.PlaceReservations.Remove(rentalAd.PlaceReservations.First(p
            => p.PlaceReservationId == @event.PlaceReservationId));

        await documentRepository.ReplaceAsync(rentalAd);

        Logger.Trace("> Rental Ad place reservation with ID: #{RentalAd} stored in the document store", @event.RentalAdId);
    }
}