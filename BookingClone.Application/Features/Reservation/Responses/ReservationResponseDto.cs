
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.Enums;

namespace BookingClone.Application.Features.Reservation.Responses;

public class ReservationResponseDto
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }


    public ReservationStatus ReservationStatus { get; set; }

    public int GetNightsNo() => (CheckOutDate - CheckInDate).Days;

    public decimal GetTotalPrice() => GetNightsNo() * RoomCardResponse.PricePerNight;

    public RoomCardResponse RoomCardResponse { get; set; } = new();

}
