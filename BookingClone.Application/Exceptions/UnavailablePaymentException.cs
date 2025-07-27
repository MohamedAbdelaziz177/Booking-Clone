
namespace BookingClone.Application.Exceptions;

public class UnavailablePaymentException : Exception
{
    public UnavailablePaymentException(string message) : base(message) { }
}
