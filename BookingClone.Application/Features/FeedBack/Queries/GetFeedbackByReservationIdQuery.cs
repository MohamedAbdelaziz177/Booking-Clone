
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Responses;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Queries;

public class GetFeedbackByReservationIdQuery : IRequest<Result<FeedbackResponse>>
{
    public int ReservationId { get; set; }
}
