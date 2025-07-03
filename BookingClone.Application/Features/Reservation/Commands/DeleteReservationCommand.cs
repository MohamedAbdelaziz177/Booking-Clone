
using BookingClone.Application.Common;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Commands;

public class DeleteReservationCommand : IRequest<Result<string>>
{
    public int Id {  get; set; }
}
