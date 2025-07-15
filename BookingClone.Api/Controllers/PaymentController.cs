using BookingClone.Application.Features.Payment.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator mediator;

        public PaymentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("pay")]
        [Authorize]
        public async Task<IActionResult> Pay(CreatePaymentCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (command.ReservationDetails.UserId != userId)
                return Forbid("U r not authorized to take this action");
            
            var res = await mediator.Send(command);
            return Ok(res);
        }
        
        [HttpPost("refund")]
        [Authorize]
        public async Task<IActionResult> Refund(RefundPaymentCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (command.userId != userId)
                return Forbid("U r not authorized to refund");

            var res = await mediator.Send(command);
            return Ok(res);
        }
    }
}
