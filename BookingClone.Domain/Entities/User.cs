﻿

using Microsoft.AspNetCore.Identity;

namespace BookingClone.Domain.Entities;
public class User : IdentityUser
{
  
    public string Firstname { get; set; } = default!;

    public string Lastname { get; set; } = default!;

    public long EmailConfirmationOtp { get; set; }

    public DateTime? EmailConfirmationOtpExpiry { get; set; } = DateTime.UtcNow;

    public long PasswordResetOtp { get; set; } = default!;

    public DateTime PasswordResetOtpExpiry { get;  set; } = DateTime.UtcNow;

    public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public List<FeedBack> FeedBacks { get; set; } = default!;
}
