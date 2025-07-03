
using BookingClone.Application.Common;
using BookingClone.Application.Features.Payment.Responses;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;

namespace BookingClone.Application.Features.Payment.Commands;

public class CreatePaymentCommand : IRequest<Result<StripeResponseDto>>
{
    public ReservationResponseDto ReservationResponse { get; set; } = default!;
}
