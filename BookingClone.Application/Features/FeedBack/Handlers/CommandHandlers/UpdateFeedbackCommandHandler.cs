
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.FeedBack.Commands;
using BookingClone.Application.Features.FeedBack.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Handlers.CommandHandlers;

public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, Result<FeedbackResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UpdateFeedbackCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<Result<FeedbackResponse>> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await unitOfWork.FeedbackRepo.GetByIdAsync((int)request.Id!);

        if (feedback == null)
            throw new EntityNotFoundException("No feedback associated to this Id");

        mapper.Map(request, feedback);

        await unitOfWork.FeedbackRepo.UpdateAsync(feedback);

        FeedbackResponse response = mapper.Map<FeedbackResponse>(feedback);

        return new Result<FeedbackResponse>(response);
    }
}
