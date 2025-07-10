using BookingClone.Application.Features.Payment.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Pay(CreatePaymentCommand command)
        {
            var res = await mediator.Send(command);
            return Ok(res);
        }
        
        [HttpPost("refund")]
        public async Task<IActionResult> Refund(RefundPaymentCommand command)
        {
            var res = await mediator.Send(command);
            return Ok(res);
        }
    }
}
