
using BookingClone.Domain.Entities;
using System.Linq.Expressions;

namespace BookingClone.Application.Specifications.RoomSpecifications;

public class RoomByHotelIdSpec : Specification<Room>
{
    private readonly int hotelId;
    public RoomByHotelIdSpec(int hotelId)
    {
        this.hotelId = hotelId;
    }

    public override Expression<Func<Room, bool>> ToExpression()
    {
        throw new NotImplementedException();
    }
}
