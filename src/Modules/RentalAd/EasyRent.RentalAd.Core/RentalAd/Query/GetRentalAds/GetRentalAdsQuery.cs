using System;
using EasyRent.Shared.Pagination;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAds;

public record GetRentalAdsQuery : PaginationQuery, IRequest<GetRentalAdsResponse>
{
    public string Country { get; init; }
    public string City { get; init; }
    public string Street { get; init; }

    public DateTime? ArrivalDate { get; init; }
    public DateTime? DepartureDate { get; init; }
}