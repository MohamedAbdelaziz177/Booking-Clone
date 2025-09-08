

using BookingClone.Domain.Entities;
using System.Linq.Expressions;

namespace BookingClone.Application.Specifications.RoomSpecifications;

public class RoomMaxPriceSpec : Specification<Room>
{
    private readonly decimal maxPrice;
    public RoomMaxPriceSpec(decimal maxPrice)
    {
        this.maxPrice = maxPrice;
    }

    public override Expression<Func<Room, bool>> ToExpression()
    {
        throw new NotImplementedException();
    }
}
