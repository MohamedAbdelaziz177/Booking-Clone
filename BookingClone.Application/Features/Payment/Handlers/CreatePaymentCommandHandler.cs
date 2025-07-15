
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Features.Payment.Commands;
using BookingClone.Application.Features.Payment.Responses;
using BookingClone.Domain.Enums;
using BookingClone.Domain.IRepositories;
using MediatR;
using PaymentEntity = BookingClone.Domain.Entities.Payment;

namespace BookingClone.Application.Features.Payment.Handlers;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<StripeResponseDto>>
{
    private readonly IStripeService stripeService;
    private readonly IUnitOfWork unitOfWork;

    public CreatePaymentCommandHandler(IStripeService stripeService, IUnitOfWork unitOfWork)
    {
        this.stripeService = stripeService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<StripeResponseDto>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var res = await stripeService.CreateStripeSession(request);

        PaymentEntity payment = new PaymentEntity();

        payment.Status = PaymentStatus.Succedded;
        payment.IntentId = res.IntentId;
        payment.ReservationId = request.ReservationResponse.Id;
        payment.Amount = request.ReservationResponse.TotalPrice;

        await unitOfWork.PaymentRepo.AddAsync(payment);

        return new Result<StripeResponseDto>(res);

    }
}
