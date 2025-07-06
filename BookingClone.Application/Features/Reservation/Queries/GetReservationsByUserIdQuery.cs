
using BookingClone.Application.Common;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Queries;

public class GetReservationsByUserIdQuery : IRequest<Result<List<ReservationResponseDto>>>
{
    public string UserId { get; set; } = string.Empty;
}
