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

    public async Task<StripeResponseDto> CreateStripeSession(CreatePaymentCommand cmd)
    {
        SessionCreateOptions options = new SessionCreateOptions()
        {
            SuccessUrl = configuration["Stripe:Success_Url"],
            CancelUrl = configuration["Stripe:Cancel_Url"],
            PaymentMethodTypes = new List<string>() { "Card" },
        };

        options.LineItems.Add(new()
        {
            PriceData = new SessionLineItemPriceDataOptions()
            {
                ProductData = new SessionLineItemPriceDataProductDataOptions()
                {
                    Name = "Reservation with Id: " + cmd.ReservationResponse.Id.ToString(),
                },
                Currency = "usd",
                UnitAmount = (long)(100 * cmd.ReservationResponse.RoomCardResponse.PricePerNight)
            },

            Quantity = cmd.ReservationResponse.GetNightsNo()
        });

        SessionService sessionService = new();
        Session session = await sessionService.CreateAsync(options);

        return new StripeResponseDto()
        {
            SessionId = session.Id,
            SessionUrl = session.Url,
            IntentId = session.PaymentIntentId
        };
        
    }


    public async Task<bool> Refund(RefundPaymentCommand cmd)
    {
        Payment? p = await unitOfWork.PaymentRepo.GetPaymentByReservatioIdAsync(cmd.ReservationId);

        if (p == null)
            return false;

        p.Status = PaymentStatus.Refunded;

        await unitOfWork.SaveChangesAsync();

        RefundCreateOptions options = new RefundCreateOptions() { PaymentIntent = p.IntentId,
            Reason = cmd.Reason };

        RefundService service = new RefundService();
        await service.CreateAsync(options);
      
        return true;
    }
}
