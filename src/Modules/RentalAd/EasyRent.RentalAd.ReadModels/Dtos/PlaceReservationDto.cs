using System;

namespace EasyRent.RentalAd.ReadModels.Dtos;

public record PlaceReservationDto
{
    public Guid PlaceReservationId { get; init; }
    public DateTime ArrivalDate { get; init; }
    public DateTime DepartureDate { get; init; }
    public string ReviewDescription { get; init; }
    public int ReviewScore { get; init; }
    public string State { get; init; }
}