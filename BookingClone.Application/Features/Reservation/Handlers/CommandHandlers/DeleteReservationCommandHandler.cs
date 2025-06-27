

using BookingClone.Application.Features.Reservation.Commands;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Handlers.CommandHandlers;

public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
{
    public Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
