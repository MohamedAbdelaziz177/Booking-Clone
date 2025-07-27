
using BookingClone.Domain.Enums;

namespace BookingClone.Domain.Entities;

public class Payment
{
    public int Id { get; set; }

    public decimal Amount {  get; set; }

    public PaymentStatus Status {  get; set; } = PaymentStatus.Succedded;

    public string IntentId { get; set; } = string.Empty;

    public int ReservationId {  get; set; }

    public Reservation Reservation { get; set; } = default!;

}
