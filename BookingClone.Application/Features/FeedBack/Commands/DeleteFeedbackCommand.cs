
using BookingClone.Application.Common;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Commands;

public class DeleteFeedbackCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}
