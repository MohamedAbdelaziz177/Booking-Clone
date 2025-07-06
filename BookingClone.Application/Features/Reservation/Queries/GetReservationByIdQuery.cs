

using BookingClone.Application.Common;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Queries;
public class GetReservationByIdQuery : IRequest<Result<ReservationResponseDto>>
{
    public int Id { get; set; }
}
