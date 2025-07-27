using BookingClone.Application.Common;
using BookingClone.Application.Features.FeedBack.Commands;
using BookingClone.Application.Features.FeedBack.Queries;
using BookingClone.Application.Features.FeedBack.Responses;
using BookingClone.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Configuration;
using System.Security.Claims;

namespace BookingClone.Api.Controllers
{
    /// <summary>
    /// Manages feedbacks for rooms and reservations.
    /// </summary>
   
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("SlidingWindow")]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IValidator validator;

        public FeedbackController(IMediator mediator, IValidator validator)
        {
            this.mediator = mediator;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves a specific feedback by its ID.
        /// </summary>
        /// <param name="Id">The feedback ID.</param>
        /// <returns>The feedback if found.</returns>
        [HttpGet("{Id:int}")]
        [ProducesResponseType(typeof(Result<FeedbackResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var query = new GetFeedbackByIdQuery() { Id = Id };

            var res = await mediator.Send(query);

            return Ok(res);
        }

        /// <summary>
        /// Retrieves feedbacks associated with a specific room.
        /// </summary>
        /// <param name="roomId">The room ID.</param>
        /// <returns>List of feedbacks for the given room.</returns>
        [HttpGet("room")]
        [ProducesResponseType(typeof(Result<List<FeedbackResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByRoomId([FromQuery] int roomId)
        {
            var query = new GetFeedbackByRoomIdQuery() { RoomId = roomId };

            var res = await mediator.Send(query);

            return Ok(res);
        }

        /// <summary>
        /// Retrieves feedbacks associated with a specific reservation.
        /// </summary>
        /// <param name="reservationId">The reservation ID.</param>
        /// <returns>Feedback for the reservation.</returns>
        [HttpGet("reservation")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Result<List<FeedbackResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByReservationId([FromQuery] int reservationId)
        {
            var query = new GetFeedbackByReservationIdQuery() { ReservationId = reservationId };

            var res = await mediator.Send(query);

            return Ok(res);
        }

        /// <summary>
        /// Adds a new feedback. Only authenticated users can submit feedback.
        /// </summary>
        /// <param name="command">The feedback details.</param>
        /// <returns>201 Created if added successfully.</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddFeedback(CreateFeedbackCommand command)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            command.UserId = userId;

            await mediator.Send(command);

            return Created();
        }

        /// <summary>
        /// Updates an existing feedback. Only the owner can update their feedback.
        /// </summary>
        /// <param name="Id">The feedback ID.</param>
        /// <param name="command">Updated feedback details.</param>
        /// <returns>The updated feedback.</returns>
        [HttpPut("{Id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(Result<FeedbackResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateFeedback([FromRoute] int Id, UpdateFeedbackCommand command)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            command.Id = Id;
            command.userId = userId;

            var res = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new {Id =  Id}, res);
        }

        /// <summary>
        /// Deletes a feedback. Only the owner or admin can delete feedback.
        /// </summary>
        /// <param name="Id">The feedback ID.</param>
        /// <returns>Success message after deletion.</returns>
        [HttpDelete("{Id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(Result<FeedbackResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFeedback([FromRoute] int Id)
        {
            var query = new DeleteFeedbackCommand() { Id = Id };

            var res = await mediator.Send(query);

            return Ok(res);
        }
    }
}
