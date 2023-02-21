using System;
using System.Collections.Generic;
using EasyRent.Infrastructure.Abstractions.DocumentStore;
using EasyRent.Infrastructure.DocumentStore;
using EasyRent.RentalAd.ReadModels.Dtos;

namespace EasyRent.RentalAd.Infrastructure.DocumentStore.Documents;

[BsonCollection("RentalAds")]
public class RentalAdDocument : BaseDocument
{
    public string RentalAdId { get; set; }
    public Guid PlaceAddressId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal PricePerDay { get; set; }
    public int AverageReviewScore { get; set; }
    public string State { get; set; }

    public PlaceOwnerDto PlaceOwner { get; set; }
    public PlacePictureDto MainPlacePicture { get; set; }
    public PlaceAddressDto PlaceAddress { get; set; }

    public ICollection<PlaceFeatureDto> PlaceFeatures { get; set; } = new List<PlaceFeatureDto>();
    public ICollection<PlacePictureDto> PlacePictures { get; set; } = new List<PlacePictureDto>();
    public ICollection<PlaceReservationDocumentDto> PlaceReservations { get; set; } = new List<PlaceReservationDocumentDto>();
}