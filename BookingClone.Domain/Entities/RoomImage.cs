

using System.Text.Json.Serialization;

namespace BookingClone.Domain.Entities;

public class RoomImage
{
    public int Id { get; set; }

    public string ImgUrl { get; set; } = string.Empty;

    [JsonIgnore]
    public Room Room { get; set; } = default!;

    public int RoomId { get; set; }
}
