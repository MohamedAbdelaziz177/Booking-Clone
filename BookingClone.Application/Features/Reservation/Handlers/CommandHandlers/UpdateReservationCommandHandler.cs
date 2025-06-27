

using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Handlers.CommandHandlers;

public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, ReservationResponseDto>
{
    public Task<ReservationResponseDto> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
