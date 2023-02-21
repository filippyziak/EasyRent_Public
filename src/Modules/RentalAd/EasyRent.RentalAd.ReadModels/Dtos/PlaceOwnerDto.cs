using System;

namespace EasyRent.RentalAd.ReadModels.Dtos;

public record PlaceOwnerDto
{
    public Guid PlaceOwnerId { get; init; }
    public string EmailAddress { get; init; }
    public string PictureUrl { get; init; }
}