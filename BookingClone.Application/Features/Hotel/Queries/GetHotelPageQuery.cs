

using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Responses;
using MediatR;

namespace BookingClone.Application.Features.Hotel.Queries;

public class GetHotelPageQuery : PaginatedQuery<Result<List<HotelResponseDto>>>
{
}
