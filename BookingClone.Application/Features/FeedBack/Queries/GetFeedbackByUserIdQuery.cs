
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Responses;

namespace BookingClone.Application.Features.FeedBack.Queries;

public class GetFeedbackByUserIdQuery : List<Result<FeedbackResponse>>
{
    public string UserId { get; set; } = default!;
}
