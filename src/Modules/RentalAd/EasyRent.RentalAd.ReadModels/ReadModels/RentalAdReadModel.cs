using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using EasyRent.RentalAd.ReadModels.Dtos;

namespace EasyRent.RentalAd.ReadModels.ReadModels;

public record RentalAdReadModel
{
    public Guid RentalAdId { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public decimal PricePerDay { get; init; }
    public double AverageReviewScore { get; init; }
    public string State { get; init; }
    
    public PlacePictureDto MainPlacePicture { get; init; }
    public PlaceOwnerDto PlaceOwner { get; init; }
    public PlaceAddressDto PlaceAddress { get; init; }
    public IReadOnlyList<DateTime> ReservedDates { get; init; } = ImmutableList<DateTime>.Empty;

    public IReadOnlyList<PlacePictureDto> PlacePictures { get; init; } = new List<PlacePictureDto>();
    public IReadOnlyList<PlaceFeatureDto> PlaceFeatures { get; init; } = new List<PlaceFeatureDto>();
    public IReadOnlyList<PlaceReservationDto> PlaceReservations { get; init; } = new List<PlaceReservationDto>();
}