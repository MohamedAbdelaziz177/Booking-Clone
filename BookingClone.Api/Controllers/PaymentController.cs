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

        /// <summary>
        /// Make a payment for a reservation.
        /// </summary>
        /// <param name="command">Payment details including reservation and user info.</param>
        /// <returns>Stripe payment session info or error.</returns>
        /// <response code="200">Payment session created successfully.</response>
        /// <response code="403">User is not allowed to pay for this reservation.</response>
        /// <response code="401">Unauthorized - missing or invalid token.</response>
        [HttpPost("pay")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Pay([FromBody] CreatePaymentCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (command.ReservationDetails.UserId != userId)
                return Forbid("You are not authorized to perform this action");

            var res = await mediator.Send(command);
            return Ok(res);
        }

        /// <summary>
        /// Request a refund for a completed payment.
        /// </summary>
        /// <param name="command">Refund request info.</param>
        /// <returns>Refund result from payment service.</returns>
        /// <response code="200">Refund processed successfully.</response>
        /// <response code="403">User is not allowed to request this refund.</response>
        /// <response code="401">Unauthorized - missing or invalid token.</response>
        [HttpPost("refund")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refund([FromBody] RefundPaymentCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (command.userId != userId)
                return Forbid("You are not authorized to request this refund");

            var res = await mediator.Send(command);
            return Ok(res);
        }
    }
}
