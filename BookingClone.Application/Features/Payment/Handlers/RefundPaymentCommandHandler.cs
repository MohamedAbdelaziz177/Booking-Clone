
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Payment.Commands;
using BookingClone.Application.Features.Payment.Responses;
using BookingClone.Domain.Enums;
using BookingClone.Domain.IRepositories;
using MediatR;
using PaymentEntity = BookingClone.Domain.Entities.Payment;

namespace BookingClone.Application.Features.Payment.Handlers
{
    internal class RefundPaymentCommandHandler : IRequestHandler<RefundPaymentCommand, Result<string>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStripeService stripeService;

        public RefundPaymentCommandHandler(IUnitOfWork unitOfWork, IStripeService stripeService)
        {
            this.unitOfWork = unitOfWork;
            this.stripeService = stripeService;
        }
        public async Task<Result<string>> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
        {
            PaymentEntity? p = await unitOfWork
                .PaymentRepo
                .GetPaymentByReservatioIdAsync(request.ReservationId);

            if (p == null || p.Status != PaymentStatus.Succedded)
                throw new UnavailablePaymentException("This Reservation is not paid");

            p.Status = PaymentStatus.Refunded;

            await unitOfWork.PaymentRepo.UpdateAsync(p);

            bool succ = await stripeService.Refund(p.IntentId);

            if (!succ)
                throw new EntityNotFoundException("Reservation not found");

            return new Result<string>("Money Refunded successfully");
        }
    }
}
