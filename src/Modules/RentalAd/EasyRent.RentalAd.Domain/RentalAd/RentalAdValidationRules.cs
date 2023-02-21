namespace EasyRent.RentalAd.Domain.RentalAd;

public static class RentalAdValidationRules
{
    public static class Picture
    {
        public const int MaxPictureCount = 10;
    }
    public static class PricePerDay
    {
        public const decimal MinPrice = 10;
        public const decimal MaxPrice = 99999;
    }

    public static class Score
    {
        public const double MinAverageScore = 1;
        public const int MinScore = 1;
        public const double MaxAverageScore = 5;
        public const int MaxScore = 5;
    }

    public static class ReservationReview
    {
        public const int MinLength = 1;
        public const int MaxLength = 500;
    }

    public static class Description
    {
        public const int MinLength = 5;
        public const int MaxLength = 500;
    }

    public static class Title
    {
        public const int MinLength = 5;
        public const int MaxLength = 255;
    }

    public static class PlaceFeature
    {
        public static class Description
        {
            public const int MinLength = 5;
            public const int MaxLength = 100;
        }
    }

    public static class PlaceAddress
    {
        public const int MinLength = 5;
        public const int MaxLength = 255;
    }
}