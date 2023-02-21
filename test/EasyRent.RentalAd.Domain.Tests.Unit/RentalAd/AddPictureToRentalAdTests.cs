using System;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;

namespace EasyRent.RentalAd.Domain.Tests.Unit.RentalAd;

public class AddPictureToRentalAdTests
{
    [Fact]
    public void AddPlacePicture_WhenFirstPictureAdded_ShouldAddPictureAndSetItAsMain()
    {
        //Asert
        var rentalAdId = Guid.NewGuid();
        var placePictureId = Guid.NewGuid();
        var url = "http://www.example.com/index.html";

        var rentalAd = new Domain.RentalAd.RentalAd(new RentalAdId(rentalAdId),
            new PlaceOwnerId(Guid.NewGuid()),
            PlaceOwnerEmailAddress.FromString("testEmail@email.eu"),
            RentalAdTitle.FromString("tile"),
            RentalAdDescription.FromString("description"),
            new PlaceAddressId(Guid.NewGuid()),
            PlaceAddressCountry.FromString("Poland"),
            PlaceAddressCity.FromString("Gliwice"),
            PlaceAddressStreet.FromString("Dworcowa 20"),
            RentalAdPricePerDay.FromDecimal(100));

        //Act
        rentalAd.AddPicture(new PlacePictureId(placePictureId),
            PlacePictureUrl.FromString(url));

        var testRentalAd = rentalAd;
    }
}