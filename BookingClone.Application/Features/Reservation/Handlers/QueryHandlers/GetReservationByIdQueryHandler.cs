
using BookingClone.Application.Features.Reservation.Queries;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Handlers.QueryHandlers;

public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationResponseDto>
{
    public Task<ReservationResponseDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
