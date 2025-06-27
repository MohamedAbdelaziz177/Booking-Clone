

using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Responses;
using MediatR;

namespace BookingClone.Application.Features.Room.Queries;

public class GetRoomPageQuery : PaginatedQuery<Result<List<RoomResponseDto>>>
{
}
