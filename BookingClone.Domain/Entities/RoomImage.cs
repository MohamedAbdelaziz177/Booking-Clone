

namespace BookingClone.Domain.Entities;

public class RoomImage
{
    public int Id { get; set; }

    public string ImgUrl { get; set; } = string.Empty;

    public Room Room { get; set; } = default!;

    public int RoomId { get; set; }
}
