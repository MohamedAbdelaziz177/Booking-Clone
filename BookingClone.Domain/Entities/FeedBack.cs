﻿
namespace BookingClone.Domain.Entities;
public class FeedBack
{
    public int Id { get; set; }

    public double Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int ReservationId { get; set; }

    public Reservation Reservation { get; set; } = new Reservation();

    public string UserId { get; set; } = default!;

    public User user { get; set; } = default!;

}
