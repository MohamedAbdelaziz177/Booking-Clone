
using System.Text.Json.Serialization;

namespace BookingClone.Domain.Entities;

public class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty ;

    public string City {  get; set; } = string.Empty;

    public string Country { get; set; } = "Egypt";

    public string Phone { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Room> Rooms { get; set; } = default!;
}
