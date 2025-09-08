
using BookingClone.Domain.Entities;
using System.Linq.Expressions;

namespace BookingClone.Application.Specifications.RoomSpecifications;

public class RoomMinPriceSpec : Specification<Room>
{
    private readonly decimal minPrice;
    public RoomMinPriceSpec(decimal minPrice)
    {
        this.minPrice = minPrice;
    }

    public override Expression<Func<Room, bool>> ToExpression()
    {
        throw new NotImplementedException();
    }
}
