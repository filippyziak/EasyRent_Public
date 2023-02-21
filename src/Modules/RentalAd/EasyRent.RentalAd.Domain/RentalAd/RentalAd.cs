using System;
using System.Collections.Generic;
using System.Linq;
using EasyRent.EventSourcing;
using EasyRent.RentalAd.Domain.RentalAd.DomainEvents.V1;
using EasyRent.RentalAd.Domain.RentalAd.Entities;
using EasyRent.RentalAd.Domain.RentalAd.States;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects;
using EasyRent.RentalAd.Domain.RentalAd.ValueObjects.Ids;
using EasyRent.Shared.Exceptions;

namespace EasyRent.RentalAd.Domain.RentalAd;

public class RentalAd : AggregateRoot<RentalAdId>
{
    public RentalAdId Id { get; internal set; }
    public PlaceOwner PlaceOwner { get; internal set; }
    public RentalAdTitle Title { get; internal set; }
    public RentalAdDescription Description { get; internal set; }
    public RentalAdPricePerDay PricePerDay { get; internal set; }
    public RentalAdAverageReviewScore AverageReviewScore { get; internal set; }

    public PlacePicture MainPlacePicture { get; internal set; }
    public PlaceAddress PlaceAddress { get; internal set; }

    public ICollection<PlaceFeature> PlaceFeatures { get; } = new List<PlaceFeature>();
    public ICollection<PlacePicture> PlacePictures { get; } = new List<PlacePicture>();
    public ICollection<PlaceReservation> PlaceReservations { get; } = new List<PlaceReservation>();

    public RentalAdState State { get; internal set; }

    protected RentalAd()
    {
    }

    public RentalAd(RentalAdId id,
        PlaceOwnerId placeOwnerId,
        PlaceOwnerEmailAddress placeOwnerEmailAddress,
        RentalAdTitle title,
        RentalAdDescription description,
        PlaceAddressId placeAddressId,
        PlaceAddressCountry placeAddressCountry,
        PlaceAddressCity placeAddressCity,
        PlaceAddressStreet placeAddressStreet,
        RentalAdPricePerDay pricePerDay)
    {
        Apply(new RentalAdCreated(
            id,
            placeOwnerId,
            placeAddressId,
            title,
            description,
            pricePerDay,
            placeAddressCountry,
            placeAddressCity,
            placeAddressStreet));

        Apply(new RentalAdPlaceOwnerCreated(
            id,
            placeOwnerId,
            placeOwnerEmailAddress));

        Apply(new RentalAdPlaceAddressCreated(
            id,
            placeAddressId,
            placeAddressCountry,
            placeAddressCity,
            placeAddressStreet));
    }

    public void UpdateTitle(RentalAdTitle adTitle)
    {
        EnsureNotArchived();
        Apply(new RentalAdTitleUpdated(Id, adTitle));
    }

    public void UpdateDescription(RentalAdDescription adDescription)
    {
        EnsureNotArchived();
        Apply(new RentalAdDescriptionUpdated(Id, adDescription));
    }

    public void UpdatePricePerDay(RentalAdPricePerDay pricePerDay)
    {
        EnsureNotArchived();
        Apply(new RentalAdPricePerDayUpdated(Id, pricePerDay));
    }

    public void AddPicture(PlacePictureId placePictureId,
        PlacePictureUrl placePictureUrl)
    {
        EnsureNotArchived();

        if (PlacePictures.Count >= RentalAdValidationRules.Picture.MaxPictureCount)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        Apply(new RentalAdPictureAdded(
            Id,
            placePictureId,
            placePictureUrl));

        if (PlacePictures.Count == 1)
        {
            Apply(new RentalAdMainPictureSet(Id, placePictureId));
        }
    }

    public void AddPlaceFeature(PlaceFeatureId placeFeatureId,
        PlaceFeatureDescription description,
        PlaceFeaturePictureUrl pictureUrl)
    {
        EnsureNotArchived();

        if (PlaceFeatures.Any(p => p.PlaceFeatureId == placeFeatureId))
        {
            throw new EntityAlreadyExistsException(placeFeatureId, typeof(PlaceFeature));
        }

        Apply(new RentalAdPlaceFeatureAdded(Id,
            placeFeatureId,
            description,
            pictureUrl));
    }

    public void RemovePlaceFeature(PlaceFeatureId placeFeatureId)
    {
        EnsureNotArchived();

        if (PlaceFeatures.Any(p => p.PlaceFeatureId == placeFeatureId))
        {
            Apply(new RentalAdPlaceFeatureRemoved(Id,
                placeFeatureId));
        }
    }

    public void AddPlaceReservation(PlaceReservationId placeReservationId,
        PlaceReservationPeriodDates periodDates)
    {
        EnsureNotArchived();

        if (!PlaceReservations.Any(pr => (pr.PeriodDates.ArrivalDate <= periodDates.ArrivalDate
                                          && pr.PeriodDates.DepartureDate >= periodDates.ArrivalDate)
                                         || (pr.PeriodDates.ArrivalDate <= periodDates.DepartureDate
                                             && pr.PeriodDates.DepartureDate >= periodDates.DepartureDate)
                                         || (pr.PeriodDates.ArrivalDate >= periodDates.ArrivalDate
                                             && pr.PeriodDates.DepartureDate >= periodDates.ArrivalDate
                                             && pr.PeriodDates.ArrivalDate <= periodDates.DepartureDate
                                             && pr.PeriodDates.DepartureDate <= periodDates.DepartureDate)))
        {
            Apply(new RentalAdPlaceReservationCreated(Id,
                placeReservationId,
                periodDates.ArrivalDate,
                periodDates.DepartureDate));
        }
    }

    public void RemovePlaceReservation(PlaceReservationId placeReservationId)
    {
        EnsureNotArchived();

        if (PlaceReservations.Any(p => p.PlaceReservationId == placeReservationId))
        {
            Apply(new RentalAdPlaceReservationRemoved(Id,
                placeReservationId));
        }
    }

    public void RemovePlacePicture(PlacePictureId pictureId)
    {
        EnsureNotArchived();

        if (PlacePictures.Any(p => p.PlacePictureId == pictureId))
        {
            Apply(new RentalAdPlacePictureRemoved(Id, pictureId));
        }
        
        if (pictureId == MainPlacePicture.PlacePictureId && PlacePictures.Any())
        {
            Apply(new RentalAdMainPictureSet(Id, PlacePictures.First().PlacePictureId));
        }

        if (pictureId == MainPlacePicture.PlacePictureId && !PlacePictures.Any())
        {
            Apply(new RentalAdMainPictureRemoved(Id, pictureId));
        }
    }


    public void Archive()
    {
        EnsureNotArchived();
        Apply(new RentalAdArchived(Id));
    }

    protected override void EnsureValidState()
    {
        var valid = Id is not null
                    && Title is not null
                    && Description is not null
                    && PricePerDay is not null
                    && PlacePictures is not null;

        if (!valid)
        {
            throw new InvalidEntityStateException(this, "RentalAd validation failed");
        }
    }

    private void EnsureNotArchived()
    {
        if (State == RentalAdState.Archived)
        {
            throw new InvalidEntityStateException(this, "RentalAd is archived");
        }
    }
}