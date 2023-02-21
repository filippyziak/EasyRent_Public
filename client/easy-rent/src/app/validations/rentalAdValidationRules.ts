export abstract class RentalAdValidationRules {
    static readonly PictureMaxPictureCount = 10;
    static readonly PricePerDayMinPrice = 10;
    static readonly PricePerDayMaxPrice = 9999;
    static readonly DescriptionMinLength = 5;
    static readonly DescriptionMaxLength = 500;
    static readonly TitleMinLength = 5;
    static readonly TitleMaxLength = 255;
    static readonly PlaceAddressMinLength = 5;
    static readonly PlaceAddressMaxLength = 255;
  }