

using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Responses;
using MediatR;

namespace BookingClone.Application.Features.Hotel.Commands;

public class DeleteHotelCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}
