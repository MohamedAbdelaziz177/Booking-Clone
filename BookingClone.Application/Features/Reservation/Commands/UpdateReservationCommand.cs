
using BookingClone.Application.Common;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Commands;

public class UpdateReservationCommand : IRequest<Result<ReservationResponseDto>>
{
}
