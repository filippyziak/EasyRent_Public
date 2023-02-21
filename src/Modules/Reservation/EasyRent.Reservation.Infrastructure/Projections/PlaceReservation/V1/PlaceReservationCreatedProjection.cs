using System.Threading.Tasks;
using EasyRent.DependencyInjection;
using EasyRent.EventSourcing;
using EasyRent.Infrastructure.Abstractions.Abstractions;
using EasyRent.Reservation.Domain.PlaceReservation.DomainEvents.V1;
using EasyRent.Reservation.Domain.PlaceReservation.States;
using EasyRent.Reservation.Infrastructure.DocumentStore.Documents;
using EasyRent.Reservation.Infrastructure.Repositories.DocumentStore.Abstractions;

namespace EasyRent.Reservation.Infrastructure.Projections.PlaceReservation.V1;

public class PlaceReservationCreatedProjection : DefaultProjection<PlaceReservationCreated>
{
    public PlaceReservationCreatedProjection(ILogger logger, IDIProvider diProvider) : base(logger, diProvider)
    {
    }

    protected override async Task ProjectEventAsync(PlaceReservationCreated @event)
    {
        var documentRepository = Scope.ResolveService<IPlaceReservationDocumentRepository>();
        
        if (!await documentRepository.ExistsAsync(pr => pr.PlaceReservationId == @event.PlaceReservationId))
        {
            await documentRepository.StoreAsync(new PlaceReservationDocument
            {
                PlaceReservationId = @event.PlaceReservationId,
                ArrivalDate = @event.ArrivalDate,
                DepartureDate = @event.DepartureDate,
                TenantId = @event.TenantId,
                OwnerId = @event.OwnerId,
                RentalAdId = @event.RentalAdId,
                State = PlaceReservationState.Ongoing.ToString()
            });

            Logger.Trace("> Place reservation with ID: #{ReservationId} stored in the document store", @event.PlaceReservationId);
        }
    }
}