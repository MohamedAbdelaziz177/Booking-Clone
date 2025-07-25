
using BookingClone.Application.Common;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Commands;

public class CommitReservationCheckOutCommand : IRequest<Result<string>>
{
    public int reservationId { get; set; }
}
