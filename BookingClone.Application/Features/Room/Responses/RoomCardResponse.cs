
namespace BookingClone.Application.Features.Room.Responses;

public class RoomCardResponse
{
    public string RoomNumber { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public decimal PricePerNight { get; set; }
}
