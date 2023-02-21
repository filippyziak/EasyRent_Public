using System.Linq;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.DocumentStore.Documents;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceReservationCreatedProjection : DefaultProjection<RentalAdPlaceReservationCreated>
{
    public RentalAdPlaceReservationCreatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceReservationCreated @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        if (!rentalAd.PlaceReservations.Any(p => p.PlaceReservationId == @event.PlaceReservationId))
        {
            rentalAd.PlaceReservations.Add(new PlaceReservationDocumentDto
            {
                PlaceReservationId = @event.PlaceReservationId,
                ArrivalDate = @event.ArrivalDate,
                DepartureDate = @event.DepartureDate
            });

            await documentRepository.ReplaceAsync(rentalAd);

            Logger.Trace("> Rental Ad place reservation with ID: #{RentalAd} stored in the document store", @event.RentalAdId);
        }
    }
}