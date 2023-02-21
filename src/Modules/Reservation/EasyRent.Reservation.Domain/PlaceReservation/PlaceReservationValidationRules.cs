namespace EasyRent.Reservation.Domain.PlaceReservation;

public static class PlaceReservationValidationRules
{
    public static class Score
    {
        public const int MinAverageScore = 1;
        public const int MaxAverageScore = 5;
    }

    public static class ReservationReview
    {
        public const int MinLength = 1;
        public const int MaxLength = 500;
    }

    public static class RentalAd
    {
        public static class PricePerDay
        {
            public const decimal MinPrice = 10;
            public const decimal MaxPrice = 99999;
        }
    }
}