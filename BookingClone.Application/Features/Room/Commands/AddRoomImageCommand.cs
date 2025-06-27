
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookingClone.Application.Features.Room.Commands;

public class AddRoomImageCommand : IRequest<Result<RoomResponseDto>>
{
    public int RoomId { get; set; }

    public IFormFile Image { get; set; } = default!;
}
