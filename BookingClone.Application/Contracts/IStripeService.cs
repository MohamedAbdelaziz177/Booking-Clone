
using BookingClone.Application.Features.Payment.Commands;
using BookingClone.Application.Features.Payment.Responses;
using BookingClone.Application.Features.Reservation.Responses;

namespace BookingClone.Application.Contracts;
public interface IStripeService
{
    Task<StripeResponseDto> CreatePaymentIntent(CreatePaymentCommand createPaymentCommand);
    Task<bool> Refund(string intent);
}
