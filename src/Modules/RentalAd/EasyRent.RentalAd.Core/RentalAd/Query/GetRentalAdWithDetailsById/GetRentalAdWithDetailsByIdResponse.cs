using EasyRent.RentalAd.ReadModels.ReadModels;
using EasyRent.Shared.Models;

namespace EasyRent.RentalAd.Core.RentalAd.Query.GetRentalAdWithDetailsById;

public record GetRentalAdWithDetailsByIdResponse(Error Error = null) : BaseResponse(Error)
{
    public RentalAdReadModel RentalAd { get; init; }
}