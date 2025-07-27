using BookingClone.Application.Contracts;
using BookingClone.Application.Features.Payment.Commands;
using BookingClone.Application.Features.Payment.Responses;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.Entities;
using BookingClone.Domain.Enums;
using BookingClone.Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;


namespace BookingClone.Infrastructure.Services;
public class StripeService : IStripeService
{
    private readonly IConfiguration configuration;
    private readonly IUnitOfWork unitOfWork;

    public StripeService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        this.configuration = configuration;
        this.unitOfWork = unitOfWork;
    }

    public async Task<StripeResponseDto> CreatePaymentIntent(ReservationResponseDto reservationDetails)
    {
         
        PaymentIntentCreateOptions options = new PaymentIntentCreateOptions()
        {
            Amount = (long)reservationDetails.TotalPrice,
            Currency = "usd",
            PaymentMethodTypes = new List<string>() { "card" },
         //   Customer = reservationDetails.UserId,
            Metadata = new Dictionary<string, string>()
            {
                {"reservationId", reservationDetails.Id.ToString()},
                {"userId", reservationDetails.UserId},
            }
        };

        PaymentIntentService service = new PaymentIntentService();
        PaymentIntent intent = await service.CreateAsync(options);

        return new StripeResponseDto()
        {
            IntentId = intent.Id,
            ClientSecret = intent.ClientSecret
        };
        
    }


    public async Task<bool> Refund(string IntentId)
    {
        var refundService = new RefundService();

        await refundService.CreateAsync(new RefundCreateOptions()
        {
            PaymentIntent = IntentId,
            Reason = "---------",
        });
       
        return true;
    }
}
