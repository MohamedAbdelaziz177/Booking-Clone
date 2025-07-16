using BookingClone.Application.Common;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Application.Features.Reservation.Queries;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Infrastructure.BackgroundJobs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReservationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get reservation details by ID.
        /// </summary>
        /// <param name="Id">Reservation ID</param>
        /// <returns>Reservation details if found</returns>
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetReservationById(int Id)
        {
            var query = new GetReservationByIdQuery() { Id = Id };
            Result<ReservationResponseDto> res = await mediator.Send(query);
            return Ok(res);
        }

        /// <summary>
        /// Get a paginated list of all reservations (Admin only).
        /// </summary>
        /// <param name="query">Pagination/filter query</param>
        /// <returns>Paginated list of reservations</returns>
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetReservationsPage([FromQuery] GetReservationsPageQuery query)
        {
            var res = await mediator.Send(query);
            return Ok(res);
        }

        /// <summary>
        /// Get reservations for the currently logged-in user.
        /// </summary>
        /// <returns>List of reservations for the user</returns>
        [HttpGet("my-reservations")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyReservations()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var query = new GetReservationsByUserIdQuery() { UserId = userId };
            var res = await mediator.Send(query);
            return Ok(res);
        }

        /// <summary>
        /// Create a new reservation.
        /// </summary>
        /// <param name="request">Reservation details</param>
        /// <returns>Confirmation of reservation</returns>
        [HttpPost("add")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand request)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            request.UserId = userId;

            var res = await mediator.Send(request);

            if (!res.Success)
                return BadRequest(res);

            BackgroundJob.Schedule<PaymentReminderJob>(j =>
                j.SendReminderAsync(res.Data!.Id),
                TimeSpan.FromSeconds(25));

            return CreatedAtAction(nameof(GetReservationById), new { id = res.Data!.Id }, res);
        }

        /// <summary>
        /// Update an existing reservation.
        /// </summary>
        /// <param name="request">Updated reservation data</param>
        /// <returns>Updated reservation result</returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateReservation([FromBody] UpdateReservationCommand request)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            request.UserId = userId;

            var res = await mediator.Send(request);

            if (!res.Success)
                return BadRequest(res);

            return CreatedAtAction(nameof(GetReservationById), new { id = request.Id }, res);
        }

        /// <summary>
        /// Cancel a reservation and schedule a refund reminder.
        /// </summary>
        /// <param name="Id">Reservation ID</param>
        /// <returns>Cancellation result</returns>
        [HttpPut("cancel/{Id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelReservation([FromRoute] int Id)
        {
            var cmd = new DeleteReservationCommand() { Id = Id };
            await mediator.Send(cmd);

            BackgroundJob.Schedule<RefundReminderJob>(j =>
                j.SendReminderAsync(Id),
                TimeSpan.FromSeconds(25));

            return Ok("Reservation Canceled Successfully");
        }
    }
}
