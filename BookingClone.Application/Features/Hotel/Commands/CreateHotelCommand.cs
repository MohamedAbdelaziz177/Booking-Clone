

using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Hotel.Commands;

public class CreateHotelCommand : IRequest<Result<HotelResponseDto>>
{
    public string Name { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = "Egypt";
    public string Phone { get; set; } = string.Empty;
}
