
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Payment.Commands;
using BookingClone.Application.Features.Payment.Responses;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.Enums;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using System.Linq.Expressions;
using PaymentEntity = BookingClone.Domain.Entities.Payment;

namespace BookingClone.Application.Features.Payment.Handlers;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<StripeResponseDto>>
{
    private readonly IStripeService stripeService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreatePaymentCommandHandler(IStripeService stripeService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.stripeService = stripeService;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<StripeResponseDto>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {

        var reservation = await unitOfWork.ReservationRepo.GetByIdAsync(request.reservationId);

        if (reservation == null)
            throw new EntityNotFoundException("No reservation associated with this Id");

        var ExistingPayment = await unitOfWork.PaymentRepo.GetPaymentByReservatioIdAsync(reservation.Id);

        if (ExistingPayment != null)
            throw new AlreadyPaidException($"Reservation with Id: {reservation.Id} has already been paid");

        var ReservationDetails = mapper.Map<ReservationResponseDto>(reservation);

        var res = await stripeService.CreatePaymentIntent(ReservationDetails);

        PaymentEntity payment = new PaymentEntity()
        {
            Status = PaymentStatus.Succedded,
            IntentId = res.IntentId,
            ReservationId = request.reservationId,
            Amount = ReservationDetails.TotalPrice
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
        
        return Result<StripeResponseDto>.CreateSuccessResult(res);

    }
}
