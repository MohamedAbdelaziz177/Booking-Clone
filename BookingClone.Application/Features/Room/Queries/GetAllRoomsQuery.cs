

using BookingClone.Application.Features.Room.Responses;
using MediatR;

namespace BookingClone.Application.Features.Room.Queries;

public class GetAllRoomsQuery : IRequest<List<RoomResponseDto>>
{
}
