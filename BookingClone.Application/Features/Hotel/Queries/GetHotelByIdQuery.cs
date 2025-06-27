
using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Responses;
using MediatR;

namespace BookingClone.Application.Features.Hotel.Queries;

public class GetHotelByIdQuery : IRequest<Result<HotelResponseDto>>
{
    public int id { get; set; }
}
