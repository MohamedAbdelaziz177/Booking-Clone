
namespace BookingClone.Application.Exceptions;

public class OtpNotValidException : Exception
{
    public OtpNotValidException(string message) : base(message) { }
}
