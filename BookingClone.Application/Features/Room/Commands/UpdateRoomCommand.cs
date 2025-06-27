
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.Enums;
using MediatR;

namespace BookingClone.Application.Features.Room.Commands;

public class UpdateRoomCommand : IRequest<Result<RoomResponseDto>>
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public RoomType Type { get; set; }
    public bool IsAvailable { get; set; }
    public int HotelId { get; set; }
}
