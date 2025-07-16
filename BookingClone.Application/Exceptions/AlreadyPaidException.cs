
namespace BookingClone.Application.Exceptions;

public class AlreadyPaidException : Exception
{
    public AlreadyPaidException(string message) : base(message) { }
}
