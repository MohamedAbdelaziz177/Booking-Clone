using BookingClone.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingClone.Domain.Entities;
public class Reservation
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; } = DateTime.UtcNow;

    public DateTime CheckInDate { get; set; } = DateTime.UtcNow;

    public DateTime CheckOutDate { get; set; } = DateTime.UtcNow;

    public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Pending;

    public Room Room { get; set; } = default!;

    public User User { get; set; } = default!;

    public List<FeedBack>? FeedBacks { get; set; }

}
