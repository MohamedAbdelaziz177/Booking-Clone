
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Commands;
using BookingClone.Application.Features.FeedBack.Responses;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Handlers.CommandHandlers;

public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, Result<string>>
{
    public Task<Result<string>> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
