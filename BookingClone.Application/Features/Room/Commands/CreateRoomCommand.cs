
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.Enums;
using MediatR;

namespace BookingClone.Application.Features.Room.Commands;

public class CreateRoomCommand : IRequest<Result<RoomResponseDto>>
{
    public string RoomNumber { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public RoomType Type { get; set; }
    public bool IsAvailable { get; set; } = true;
    public int HotelId { get; set; }
}
