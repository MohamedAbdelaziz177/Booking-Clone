
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Responses;
using MediatR;

namespace BookingClone.Application.Features.Room.Queries;

public class GetAllRoomsAvailableBetweenQuery : PaginatedQuery<Result<List<RoomResponseDto>>>
{
    public DateTime start {  get; set; }
    public DateTime end { get; set; }
    public int? hotelId { get; set; }
    public decimal? minPrice { get; set; } = 0;
    public decimal? maxPrice { get; set; } = decimal.MaxValue;
}
