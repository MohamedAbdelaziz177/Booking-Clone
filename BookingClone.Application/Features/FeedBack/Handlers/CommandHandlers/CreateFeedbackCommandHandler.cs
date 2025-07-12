
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Commands;
using BookingClone.Application.Features.FeedBack.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using FeedbackEntity = BookingClone.Domain.Entities.FeedBack;
namespace BookingClone.Application.Features.FeedBack.Handlers.CommandHandlers;

public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, Result<FeedbackResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateFeedbackCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<FeedbackResponse>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        FeedbackEntity feedback = mapper.Map<FeedbackEntity>(request);

        await unitOfWork.FeedbackRepo.AddAsync(feedback);

        FeedbackResponse response = mapper.Map<FeedbackResponse>(request);

        return new Result<FeedbackResponse>(response);
    }
}
