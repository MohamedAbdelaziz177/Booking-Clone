
using BookingClone.Domain.Entities;
using BookingClone.Domain.Enums;
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

    public override Expression<Func<Room, bool>> ToExpression() =>
        r => !r
        .Reservations
        .Any(rs => rs.CheckInDate > start
        && rs.CheckOutDate < end
        && rs.ReservationStatus != ReservationStatus.Cancelled);
}
