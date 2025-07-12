
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.FeedBack.Commands;
using BookingClone.Application.Features.FeedBack.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Handlers.CommandHandlers;

public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, Result<string>>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteFeedbackCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await unitOfWork.FeedbackRepo.GetByIdAsync(request.Id);

        if (feedback == null)
            throw new EntityNotFoundException("No Feedback associated to this Id");

        await unitOfWork.FeedbackRepo.DeleteAsync(feedback);

        return new Result<string>(data: "Feedback Deleted Successfully");
    }
}
