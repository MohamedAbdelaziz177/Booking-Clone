
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.FeedBack.Queries;
using BookingClone.Application.Features.FeedBack.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using FeedbackEntity = BookingClone.Domain.Entities.FeedBack;
namespace BookingClone.Application.Features.FeedBack.Handlers.QueryHandlers;

public class GetFeedbackByIdQueryHandler : IRequestHandler<GetFeedbackByIdQuery, Result<FeedbackResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetFeedbackByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<FeedbackResponse>> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
    {
        FeedbackEntity? feedBack = await unitOfWork.FeedbackRepo.GetByIdAsync(request.Id);

        if (feedBack == null)
            throw new EntityNotFoundException("No feedback found");

        var res = mapper.Map<FeedbackResponse>(feedBack);

        return new Result<FeedbackResponse>(res);
    }
}
