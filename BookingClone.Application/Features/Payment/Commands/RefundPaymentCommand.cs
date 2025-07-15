

using BookingClone.Application.Common;
using MediatR;

namespace BookingClone.Application.Features.Payment.Commands;

public class RefundPaymentCommand : IRequest<Result<string>>
{
    public int ReservationId {  get; set; }
    public string userId { get; set; } = default!;
    public string Reason { get; set; } = string.Empty!;
}
