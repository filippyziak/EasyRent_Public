using System;
using EasyRent.EventSourcing;
using EasyRent.Shared;

namespace EasyRent.RentalAd.Domain.RentalAd.ValueObjects;

public record PlaceReservationPeriodDates : ValueObject
{
    public DateTime ArrivalDate { get; }
    public DateTime DepartureDate { get; }

    internal PlaceReservationPeriodDates(DateTime arrivalDate, DateTime departureDate)
    {
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
    }

    public static PlaceReservationPeriodDates OneNight(DateTime arrivalDate)
    {
        Validator.Date.AlreadyPassed(arrivalDate);

        return new PlaceReservationPeriodDates(arrivalDate, arrivalDate.AddDays(1));
    }

    public static PlaceReservationPeriodDates ForRange(DateTime arrivalDate, DateTime departureDate)
    {
        Validator.Date.AlreadyPassed(arrivalDate);
        Validator.Date.AlreadyPassed(departureDate);

        return new PlaceReservationPeriodDates(arrivalDate, departureDate);
    }
}