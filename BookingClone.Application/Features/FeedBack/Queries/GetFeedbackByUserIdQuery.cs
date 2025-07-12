
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Responses;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Queries;

public class GetFeedbackByUserIdQuery : IRequest<Result<List<FeedbackResponse>>>
{
    public string UserId { get; set; } = default!;
}
