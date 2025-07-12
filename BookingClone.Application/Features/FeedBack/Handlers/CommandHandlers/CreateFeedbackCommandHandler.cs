
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Commands;
using BookingClone.Application.Features.FeedBack.Responses;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Handlers.CommandHandlers;

public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, Result<FeedbackResponse>>
{
    public Task<Result<FeedbackResponse>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
