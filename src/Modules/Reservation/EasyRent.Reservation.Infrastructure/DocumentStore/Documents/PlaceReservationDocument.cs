using System;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Infrastructure.DocumentStore;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyRent.Reservation.Infrastructure.DocumentStore.Documents;

[BsonCollection("PlaceReservations")]
public class PlaceReservationDocument : BaseDocument
{
    public Guid PlaceReservationId { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime ArrivalDate { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime DepartureDate { get; set; }

    public string ReviewDescription { get; set; }
    public int ReviewScore { get; set; }

    public Guid TenantId { get; set; }
    public Guid OwnerId { get; set; }
    public Guid RentalAdId { get; set; }
    public string State { get; set; }
}