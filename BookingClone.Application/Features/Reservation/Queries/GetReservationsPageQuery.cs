
using BookingClone.Application.Common;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Queries;

public class GetReservationsPageQuery : PaginatedQuery<Result<List<ReservationResponseDto>>>
{
    public DateTime date { get; set; } = DateTime.Today;

    public int? hotelId { get; set; } = null;
}
