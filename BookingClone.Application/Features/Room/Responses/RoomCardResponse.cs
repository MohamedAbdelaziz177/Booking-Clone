
namespace BookingClone.Application.Features.Room.Responses;

public class RoomCardResponse
{
    public int Id {  get; set; } 
    public string RoomNumber { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
}
