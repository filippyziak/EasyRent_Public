using System;

namespace EasyRent.RentalAd.ReadModels.Dtos;

public record PlaceAddressDto
{
    public Guid PlaceAddressId { get; init; }
    public string Country { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
}