
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Responses;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Commands;

public class CreateFeedbackCommand : IRequest<Result<FeedbackResponse>>
{
    public double Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int ReservationId { get; set; }

    public int RoomId { get; set; }

     
}
