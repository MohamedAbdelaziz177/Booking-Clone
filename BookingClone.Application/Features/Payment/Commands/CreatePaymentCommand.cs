
using BookingClone.Application.Common;
using BookingClone.Application.Features.Payment.Responses;
using BookingClone.Application.Features.Reservation.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace BookingClone.Application.Features.Payment.Commands;

public class CreatePaymentCommand : IRequest<Result<StripeResponseDto>>
{
    public int reservationId { get; set; }

    public string? userId { get; set; }
}
