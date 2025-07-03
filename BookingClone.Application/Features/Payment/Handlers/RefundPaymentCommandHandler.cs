
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Payment.Commands;
using BookingClone.Domain.IRepositories;
using MediatR;

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
            bool succ = await stripeService.Refund(request);

            if (!succ)
                throw new EntityNotFoundException("Reservation not found");

            return ResultBuilder<string>.CreateSuccessResponse(data: "Money Refunded successfully");
        }
    }
}
