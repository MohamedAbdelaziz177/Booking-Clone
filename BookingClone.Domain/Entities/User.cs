

namespace BookingClone.Domain.Entities;
public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public string Firstname { get; set; } = default!;

    public string Lastname { get; set; } = default!;

    public string Email { get; set; } = default!;

    public long EmailConfirmationOtp { get; set; }

    public DateTime EmailConfirmationOtpExpiry { get; set; } = default!;

    public long PasswordResetOtp { get; set; } = default!;

    public DateTime PasswordResetOtpExpiry { get;  set; } = default!;

    public ICollection<Role> Roles { get; set; } = new List<Role>();
}
