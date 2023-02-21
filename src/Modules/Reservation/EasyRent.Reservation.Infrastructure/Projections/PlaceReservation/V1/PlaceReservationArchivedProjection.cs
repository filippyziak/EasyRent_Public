using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.Reservation.Infrastructure.Projections.PlaceReservation.V1;

public class PlaceReservationArchivedProjection : DefaultProjection<PlaceReservationArchived>
{
    public PlaceReservationArchivedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(PlaceReservationArchived @event)
    {
        var documentRepository = Scope.ResolveService<IPlaceReservationDocumentRepository>();

        var placeReservation = await documentRepository.FindAsync(pr => pr.PlaceReservationId == @event.PlaceReservationId);

        if (placeReservation is not null)
        {
            await documentRepository.DeleteAsync(placeReservation);
            Logger.Trace("> Place reservation with ID: #{ReservationId} deleted from the document store", @event.PlaceReservationId);
        }
    }
}