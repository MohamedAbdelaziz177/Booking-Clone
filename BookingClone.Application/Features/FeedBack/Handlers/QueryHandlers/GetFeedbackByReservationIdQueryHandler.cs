
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Queries;
using BookingClone.Application.Features.FeedBack.Responses;
using MediatR;

namespace BookingClone.Application.Features.FeedBack.Handlers.QueryHandlers;

public class GetFeedbackByReservationIdQueryHandler : IRequestHandler<GetFeedbackByReservationIdQuery,
    Result<List<FeedbackResponse>>>
{
    public Task<Result<List<FeedbackResponse>>> Handle(GetFeedbackByReservationIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
