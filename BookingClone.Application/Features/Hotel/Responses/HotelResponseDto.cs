
using BookingClone.Application.Features.Room.Responses;

namespace BookingClone.Application.Features.Hotel.Responses;

public class HotelResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Country { get; set; } = "Egypt";

    public string Phone { get; set; } = string.Empty;

    public List<RoomResponseDto> Rooms { get; set; } = new List<RoomResponseDto>();
}
