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

    public async Task<StripeResponseDto> CreateStripeSession(CreatePaymentCommand cmd)
    {
        string Domain = configuration["DOMAIN"]!;
        string SuccessPage = configuration["STRIPE:SUCCESS_PAGE"]!;
        string CancelPage = configuration["STRIPE:CANCEL_PAGE"]!;

        SessionCreateOptions options = new SessionCreateOptions()
        {
            SuccessUrl = Domain + SuccessPage,
            CancelUrl = Domain + CancelPage,
            PaymentMethodTypes = new List<string>() { "card" },
            Mode = "payment",
            LineItems = new List<SessionLineItemOptions>()
        };

        options.LineItems.Add(new()
        {
            PriceData = new SessionLineItemPriceDataOptions()
            {
                ProductData = new SessionLineItemPriceDataProductDataOptions()
                {
                    Name = "Reservation with Id: " + cmd.ReservationDetails.Id.ToString(),
                },
                Currency = "usd",
                UnitAmount = (long)(100 * cmd.ReservationDetails.RoomDetails.PricePerNight)
            },

            Quantity = cmd.ReservationDetails.NightsNo
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
