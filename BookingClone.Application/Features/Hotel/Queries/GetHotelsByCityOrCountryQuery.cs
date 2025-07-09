
using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Responses;
using MediatR;

namespace BookingClone.Application.Features.Hotel.Queries;

public class GetHotelsByCityOrCountryQuery : IRequest<Result<List<HotelResponseDto>>>
{
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
