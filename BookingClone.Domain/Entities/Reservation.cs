using BookingClone.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClone.Domain.Entities;
public class Reservation
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public ReservationStatus ReservationStatus { get; set; }

    public Room Room { get; set; } = new Room();

    public User user { get; set; } = new User();

    public List<FeedBack>? FeedBacks { get; set; }

}
