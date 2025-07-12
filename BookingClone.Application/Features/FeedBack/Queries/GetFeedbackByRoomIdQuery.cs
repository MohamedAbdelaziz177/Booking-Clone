
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Responses;

namespace BookingClone.Application.Features.FeedBack.Queries;

public class GetFeedbackByRoomIdQuery : List<Result<FeedbackResponse>>
{
    public int RoomId { get; set; }
}
