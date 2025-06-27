
namespace BookingClone.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string msg) : base(msg) { }
}
