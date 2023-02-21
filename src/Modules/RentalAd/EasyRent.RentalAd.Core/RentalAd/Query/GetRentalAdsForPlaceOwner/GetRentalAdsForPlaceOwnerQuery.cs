using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAdsForPlaceOwner;

public record GetRentalAdsForPlaceOwnerQuery(Guid PlaceOwnerId) : IRequest<GetRentalAdsForPlaceOwnerResponse>;