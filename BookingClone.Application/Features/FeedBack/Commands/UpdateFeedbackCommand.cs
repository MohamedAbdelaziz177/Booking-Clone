
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Responses;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Commands;

public class UpdateFeedbackCommand : IRequest<Result<FeedbackResponse>>
{
    public int? Id;
    public double Rating { get; set; }
    public string Comment { get; set; } = string.Empty;

}
