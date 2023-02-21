using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.States;
using EasyRent.Reservation.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.Reservation.Infrastructure.Projections.PlaceReservation.V1;

public class PlaceReservationPaidProjection : DefaultProjection<PlaceReservationPaid>
{
    public PlaceReservationPaidProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(PlaceReservationPaid @event)
    {
        var documentRepository = Scope.ResolveService<IPlaceReservationDocumentRepository>();

        var placeReservation = await documentRepository.FindAsync(pr => pr.PlaceReservationId == @event.PlaceReservationId);

        if (placeReservation is not null)
        {
            placeReservation.State = PlaceReservationState.Paid.ToString();
            await documentRepository.ReplaceAsync(placeReservation);
            
            Logger.Trace("> Place reservation with ID: {PlaceReservationId} has been updated in document store", @event.PlaceReservationId);
        }
    }
}