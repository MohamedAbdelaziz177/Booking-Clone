using BookingClone.Application.Features.FeedBack.Commands;
using BookingClone.Application.Features.FeedBack.Queries;
using BookingClone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Security.Claims;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator mediator;

        public FeedbackController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var query = new GetFeedbackByIdQuery() { Id = Id };

            var res = await mediator.Send(query);

            return Ok(res);
        }

        [HttpGet("room")]
        public async Task<IActionResult> GetByRoomId([FromQuery] int roomId)
        {
            var query = new GetFeedbackByRoomIdQuery() { RoomId = roomId };

            var res = await mediator.Send(query);

            return Ok(res);
        }

        [HttpGet("reservation")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByReservationId([FromQuery] int reservationId)
        {
            var query = new GetFeedbackByReservationIdQuery() { ReservationId = reservationId };

            var res = await mediator.Send(query);

            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFeedback(CreateFeedbackCommand command)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            command.UserId = userId;

            await mediator.Send(command);

            return Created();
        }

        [HttpPut("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateFeedback([FromRoute] int Id, UpdateFeedbackCommand command)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            command.Id = Id;
            command.userId = userId;

            var res = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new {Id =  Id}, res);
        }

        [HttpDelete("{Id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteFeedback([FromRoute] int Id)
        {
            var query = new DeleteFeedbackCommand() { Id = Id };

            var res = await mediator.Send(query);

            return Ok(res);
        }
    }
}
