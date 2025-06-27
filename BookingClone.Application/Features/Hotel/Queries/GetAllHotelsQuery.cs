
using BookingClone.Application.Features.Hotel.Responses;
using MediatR;

namespace BookingClone.Application.Features.Hotel.Queries;
public class GetAllHotelsQuery : IRequest<List<HotelResponseDto>>
{
}
