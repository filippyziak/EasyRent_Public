using System;
using MediatR;

namespace EasyRent.RentalAd.Core.RentalAd.Commands.UpdateRentalAdPricePerDay;

public record UpdateRentalAdPricePerDayCommand : IRequest<UpdateRentalAdPricePerDayResponse>
{
    public Guid CurrentOwnerId { get; init; }
    public Guid RentalAdId { get; init; }
    public decimal NewPricePerDay { get; init; }
}