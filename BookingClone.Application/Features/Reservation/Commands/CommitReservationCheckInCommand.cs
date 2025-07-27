
using BookingClone.Application.Common;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Commands;

public class CommitReservationCheckInCommand : IRequest<Result<string>>
{
    public int reservationId {  get; set; }
}
