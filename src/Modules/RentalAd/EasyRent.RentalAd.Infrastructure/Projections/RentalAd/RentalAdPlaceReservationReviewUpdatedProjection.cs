using System.Linq;
using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.RentalAd.Infrastructure.Projections.RentalAd;

public class RentalAdPlaceReservationReviewUpdatedProjection : DefaultProjection<RentalAdPlaceReservationReviewUpdated>
{
    public RentalAdPlaceReservationReviewUpdatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(RentalAdPlaceReservationReviewUpdated @event)
    {
        var documentRepository = Scope.ResolveService<IRentalAdDocumentRepository>();

        var rentalAd = await documentRepository.FindAsync(r
            => r.RentalAdId == @event.RentalAdId.ToString());

        var placeReservation = rentalAd.PlaceReservations.First(r
            => r.PlaceReservationId == @event.PlaceReservationId);

        rentalAd.PlaceReservations.Remove(placeReservation);
        rentalAd.PlaceReservations.Add(placeReservation with
        {
            ReviewDescription = @event.review
        });

        await documentRepository.ReplaceAsync(rentalAd);

        Logger.Trace("> Rental Ad place reservation with ID: #{RentalAd} stored in the document store", @event.RentalAdId);
    }
}