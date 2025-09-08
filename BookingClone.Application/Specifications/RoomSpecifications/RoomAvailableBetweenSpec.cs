
using BookingClone.Domain.Entities;
using System.Linq.Expressions;

namespace BookingClone.Application.Specifications.RoomSpecifications;

public class RoomAvailableBetweenSpec : Specification<Room>
{
    private readonly DateTime start;
    private readonly DateTime end;

    public RoomAvailableBetweenSpec(DateTime start, DateTime end)
    {
        this.start = start;
        this.end = end;
    }

    public override Expression<Func<Room, bool>> ToExpression()
    {
        throw new NotImplementedException();
    }
}
