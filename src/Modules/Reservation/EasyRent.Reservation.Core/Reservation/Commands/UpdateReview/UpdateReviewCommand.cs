using System;
using MediatR;

namespace EasyRent.Reservation.Core.Reservation.Commands.UpdateReview;

public record UpdateReviewCommand : IRequest<UpdateReviewResponse>
{
    public Guid PlaceReservationId { get; init; }
    public string NewReviewDescription { get; init; }
    public int? NewReviewScore { get; init; }
}