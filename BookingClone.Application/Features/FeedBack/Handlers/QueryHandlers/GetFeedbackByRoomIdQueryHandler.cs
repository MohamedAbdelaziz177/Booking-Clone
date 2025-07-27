
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Queries;
using BookingClone.Application.Features.FeedBack.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Handlers.QueryHandlers;

public class GetFeedbackByRoomIdQueryHandler : IRequestHandler<GetFeedbackByRoomIdQuery,
    Result<List<FeedbackResponse>>>

{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetFeedbackByRoomIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<List<FeedbackResponse>>> Handle(GetFeedbackByRoomIdQuery request, CancellationToken cancellationToken)
    {
        var lst = await unitOfWork.FeedbackRepo.GetFeedBacksByRoomIdAsync(request.RoomId);

        var response = lst.Select(f => mapper.Map<FeedbackResponse>(f)).ToList();

        return new Result<List<FeedbackResponse>>(response);
    }
}
