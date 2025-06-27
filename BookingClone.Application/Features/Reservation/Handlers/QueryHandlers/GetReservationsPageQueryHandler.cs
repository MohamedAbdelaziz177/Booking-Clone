
using BookingClone.Application.Features.Reservation.Queries;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Handlers.QueryHandlers;

public class GetReservationsPageQueryHandler : IRequestHandler<GetReservationsPageQuery, List<ReservationResponseDto>>
{
    public Task<List<ReservationResponseDto>> Handle(GetReservationsPageQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
