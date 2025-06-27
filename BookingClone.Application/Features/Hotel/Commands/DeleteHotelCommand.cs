

using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Responses;
using MediatR;

namespace BookingClone.Application.Features.Hotel.Commands;

public class DeleteHotelCommand : IRequest<Result<HotelResponseDto>>
{
    public int Id { get; set; }
}
