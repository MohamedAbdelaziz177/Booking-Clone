using BookingClone.Application.Contracts;
using BookingClone.Application.Features.Payment.Commands;
using BookingClone.Application.Features.Payment.Responses;
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

    public async Task<StripeResponseDto> CreatePaymentIntent(CreatePaymentCommand cmd)
    {
         
        PaymentIntentCreateOptions options = new PaymentIntentCreateOptions()
        {
            Amount = (long)cmd.ReservationDetails.TotalPrice,
            Currency = "usd",
            PaymentMethodTypes = new List<string>() { "card" },
            Customer = cmd.ReservationDetails.UserId,
            Metadata = new Dictionary<string, string>()
            {
                {"reservationId", cmd.ReservationDetails.Id.ToString()},
                {"userId", cmd.ReservationDetails.UserId},
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
