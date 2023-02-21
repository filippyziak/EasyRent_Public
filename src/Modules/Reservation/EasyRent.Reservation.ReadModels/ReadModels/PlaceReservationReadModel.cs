using System;

namespace EasyRent.Reservation.ReadModels.ReadModels;

public record PlaceReservationReadModel
{
    public Guid PlaceReservationId { get; init; }
    public DateTime ArrivalDate { get; init; }
    public DateTime DepartureDate { get; init; }
    public string ReviewDescription { get; init; }
    public int ReviewScore { get; init; }

    public Guid TenantId { get; init; }
    public Guid RentalAdId { get; init; }
    public string State { get; init; }
}