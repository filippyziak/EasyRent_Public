using System;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyRent.RentalAd.Infrastructure.DocumentStore.Documents;

public record PlaceReservationDocumentDto
{
    public Guid PlaceReservationId { get; init; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime ArrivalDate { get; init; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime DepartureDate { get; init; }

    public string ReviewDescription { get; init; }
    public int ReviewScore { get; init; }
    public string State { get; init; }
}