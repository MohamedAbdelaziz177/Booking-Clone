
using BookingClone.Domain.Entities;

namespace BookingClone.Application.Specifications.RoomSpecifications;

public class RoomSpecs
{
    public static Specification<Room> HotelId(int hotelId) => new RoomByHotelIdSpec(hotelId);

    public static Specification<Room> MinPrice(decimal minPrice) => new RoomMinPriceSpec(minPrice);

    public static Specification<Room> MaxPrice(decimal maxPrice) => new RoomMaxPriceSpec(maxPrice);

    public static Specification<Room> AvailableBetween(DateTime start, DateTime end) => 
        new RoomAvailableBetweenSpec(start, end);
}
