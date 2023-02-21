using System;

namespace EasyRent.RentalAd.ReadModels.ReadModels;

public record PlaceOwnerReadModel
{
    public Guid PlaceOwnerId { get; init; }
    public string EmailAddress { get; init; }
}