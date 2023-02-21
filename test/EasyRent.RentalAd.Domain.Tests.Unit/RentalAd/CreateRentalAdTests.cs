using System;
using EasyRent.RentalAd.Domain.RentalAd;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.RentalAd.Domain.Tests.Unit.Factories;
using FluentAssertions.Execution;

namespace EasyRent.RentalAd.Domain.Tests.Unit.RentalAd;

public class CreateRentalAdTests
{
    private const string StubText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
    private const string StubUrl = "http://www.example.com/index.html";
    private const string StubEmailAddress = "example@email.eu";
    private const string StubCountry = "Poland";
    private const string StubCity = "Olsztyn";
    private const string StubStreet = "ul. Kujawska 142";
    private const decimal StubPricePerDay = 100;

    private readonly Guid RentalAdId = Guid.NewGuid();
    private readonly Guid PlaceOwnerId = Guid.NewGuid();
    private readonly Guid PlaceAddressId = Guid.NewGuid();

    [Fact]
    public void CreateRentalAd_WhenRentalAdIdIsEmpty_ShouldThrowArgumentException()
    {
        //Arrange
        var invalidRentalId = Guid.Empty;

        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(invalidRentalId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(StubStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateRentalAd_WhenPlaceOwnerIdIsEmpty_ShouldThrowArgumentException()
    {
        //Arrange
        var invalidPlaceOwnerId = Guid.Empty;

        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(invalidPlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(StubStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateRentalAd_WhenPlaceOwnerEmailAddressIsNullOrEmpty_ShouldThrowArgumentException(string emailAddress)
    {
        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(emailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(StubStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("@example@email.eu")]
    [InlineData("example@emaileu")]
    [InlineData("example.emaileu")]
    [InlineData("example.email.eu")]
    [InlineData("example@email/eu")]
    public void CreateRentalAd_WhenPlaceOwnerEmailAddressIsIncorrect_ShouldThrowFormatException(string emailAddress)
    {
        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(emailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(StubStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<FormatException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateRentalAd_WhenPlaceOwnerPictureUrlIsNullOrEmpty_ShouldThrowFormatException(string pictureUrl)
    {
        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(StubStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("http:///www.example.com/index.html")]
    [InlineData("www.examplecom/index.html")]
    public void CreateRentalAd_WhenPlaceOwnerPictureUrlIsIncorrect_ShouldThrowArgumentException(string pictureUrl)
    {
        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(StubStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void CreateRentalAd_WhenPlaceAddressIdIsEmpty_ShouldThrowArgumentException()
    {
        //Arrange
        var invalidPlaceAddressId = Guid.Empty;

        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(invalidPlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(StubStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateRentalAd_WhenPlaceAddressStreetIsNullOrEmpty_ShouldThrowArgumentException(string placeAddressStreet)
    {
        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(placeAddressStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateRentalAd_WhenPlaceAddressStreetIsBeyondMaxLength_ShouldThrowArgumentOutOfRangeException()
    {
        //Arrange
        var tooBigString = StringFactory.GenerateString(RentalAdValidationRules.PlaceAddress.MaxLength + 1);

        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(tooBigString),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void CreateRentalAd_WhenPlaceAddressStreetIsShorterThanMinLength_ShouldThrowArgumentOutOfRangeException()
    {
        //Arrange
        var tooShortString = StringFactory.GenerateString(RentalAdValidationRules.PlaceAddress.MinLength - 1);

        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(tooShortString),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(".Poland")]
    [InlineData("Poland@")]
    [InlineData("Poland1")]
    public void CreateRentalAd_WhenPlaceAddressCountryContainsNotOnlyLetters_ShouldThrowFormatException(string countryName)
    {
        //Act
        Action act = () => new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(countryName),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(StubStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void CreateRentalAd_WhenCreated_ShouldCreateRentalAd()
    {
        //Act
        var rentalAd = new Domain.RentalAd.RentalAd(new RentalAdId(RentalAdId),
            new PlaceOwnerId(PlaceOwnerId),
            PlaceOwnerEmailAddress.FromString(StubEmailAddress),
            RentalAdTitle.FromString(StubText),
            RentalAdDescription.FromString(StubText),
            new PlaceAddressId(PlaceAddressId),
            PlaceAddressCountry.FromString(StubCountry),
            PlaceAddressCity.FromString(StubCity),
            PlaceAddressStreet.FromString(StubStreet),
            RentalAdPricePerDay.FromDecimal(StubPricePerDay));

        //Assert
        using (new AssertionScope())
        {
            rentalAd.Id.Value.Should().Be(RentalAdId);
            rentalAd.PlaceOwner.RentalAdId.Value.Should().Be(RentalAdId);
            rentalAd.PlaceOwner.PlaceOwnerId.Value.Should().Be(PlaceOwnerId);
            rentalAd.PlaceOwner.EmailAddress.Value.Should().Be(StubEmailAddress);
            rentalAd.PlaceOwner.PictureUrl.Value.Should().Be(StubUrl);
            rentalAd.Description.Value.Should().Be(StubText);
            rentalAd.Title.Value.Should().Be(StubText);
            rentalAd.PlaceAddress.PlaceAddressId.Value.Should().Be(PlaceAddressId);
            rentalAd.PlaceAddress.RentalAdId.Value.Should().Be(RentalAdId);
            rentalAd.PlaceAddress.Country.Value.Should().Be(StubCountry);
            rentalAd.PlaceAddress.City.Value.Should().Be(StubCity);
            rentalAd.PlaceAddress.Street.Value.Should().Be(StubStreet);
            rentalAd.PricePerDay.Value.Should().Be(StubPricePerDay);
        }
    }
}