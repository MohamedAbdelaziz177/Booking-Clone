

using BookingClone.Application.Common;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClone.Application.Features.Reservation.Commands;

public class CreateReservationCommand : IRequest<Result<ReservationResponseDto>>
{
    
    public int RoomId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; } = DateTime.Now;
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

}
