
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Queries;

public class GetAllReservationsQuery : IRequest<List<ReservationResponseDto>>
{
}
