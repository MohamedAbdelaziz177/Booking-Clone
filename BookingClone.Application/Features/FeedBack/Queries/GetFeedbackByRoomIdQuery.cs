
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Responses;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Queries;

public class GetFeedbackByRoomIdQuery : IRequest<Result<List<FeedbackResponse>>>
{
    public int RoomId { get; set; }
}
