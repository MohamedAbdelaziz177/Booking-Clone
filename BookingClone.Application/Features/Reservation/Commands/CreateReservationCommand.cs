

using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Commands;

public class CreateReservationCommand : IRequest<ReservationResponseDto>
{
}
