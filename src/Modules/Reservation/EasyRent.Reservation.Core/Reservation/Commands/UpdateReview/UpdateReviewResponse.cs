using EasyRent.Shared.Models;

namespace EasyRent.Reservation.Core.Reservation.Commands.UpdateReview;

public record UpdateReviewResponse(Error Error = null) : BaseResponse(Error);