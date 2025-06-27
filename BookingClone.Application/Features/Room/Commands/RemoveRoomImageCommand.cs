
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Responses;
using MediatR;

namespace BookingClone.Application.Features.Room.Commands;

public class RemoveRoomImageCommand : IRequest<Result<RoomResponseDto>>
{
    public int RoomId { get; set; }
    public int ImageId { get; set; }
}
