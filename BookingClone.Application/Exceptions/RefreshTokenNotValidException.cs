
namespace BookingClone.Application.Exceptions;

public class RefreshTokenNotValidException : Exception
{
    public RefreshTokenNotValidException(string message) : base(message) { }
}
