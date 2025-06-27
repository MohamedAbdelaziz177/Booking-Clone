

using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Queries;
public class GetReservationByIdQuery : IRequest<ReservationResponseDto>
{
    public int Id { get; set; }
}
