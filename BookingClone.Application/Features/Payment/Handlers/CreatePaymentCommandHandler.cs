
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Payment.Commands;
using BookingClone.Application.Features.Payment.Responses;
using BookingClone.Domain.Enums;
using BookingClone.Domain.IRepositories;
using MediatR;
using System.Linq.Expressions;
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
        var res = await stripeService.CreatePaymentIntent(request);

        var reservation = await unitOfWork.ReservationRepo.GetByIdAsync(request.ReservationDetails.Id);

        if (reservation == null)
            throw new EntityNotFoundException("No reservation associated with this Id");

        var ExistingPayment = await unitOfWork.PaymentRepo.GetPaymentByReservatioIdAsync(reservation.Id);

        if (ExistingPayment != null)
            throw new AlreadyPaidException($"Reservation with Id: {reservation.Id} has already been paid");

        PaymentEntity payment = new PaymentEntity()
        {
            Status = PaymentStatus.Succedded,
            IntentId = res.IntentId,
            ReservationId = request.ReservationDetails.Id,
            Amount = request.ReservationDetails.TotalPrice
        };

        using var Trx = await unitOfWork.GetTransaction();

        try
        {
            await unitOfWork.PaymentRepo.AddAsync(payment);

            reservation.ReservationStatus = ReservationStatus.Confirmed;
            await unitOfWork.ReservationRepo.UpdateAsync(reservation);

            await Trx.CommitAsync();
        }
        catch
        {
            await Trx.RollbackAsync();
            throw;
        }
        
        return new Result<StripeResponseDto>(res);

    }
}
