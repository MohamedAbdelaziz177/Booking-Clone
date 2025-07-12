
using BookingClone.Domain.Enums;

namespace BookingClone.Domain.Entities;

public class Room
{
    public int Id { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public int Capacity {  get; set; }

    public decimal PricePerNight { get; set; }

    public RoomType Type { get; set; }

    public bool isAvailable { get; set; } = true;

    public int HotelId { get; set; }

    public Hotel Hotel { get; set; } = default!;

    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    public List<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
    //public List<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

}
