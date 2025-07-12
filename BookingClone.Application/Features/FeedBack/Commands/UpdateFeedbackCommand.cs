
using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookingClone.Application.Features.FeedBack.Commands;

public class UpdateFeedbackCommand : IRequest<Result<FeedbackResponse>>
{
}
