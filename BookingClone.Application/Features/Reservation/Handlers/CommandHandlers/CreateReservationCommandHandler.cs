
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Handlers.CommandHandlers;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ReservationResponseDto>
{
    public Task<ReservationResponseDto> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
