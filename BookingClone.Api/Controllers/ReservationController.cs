using BookingClone.Application.Common;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Application.Features.Reservation.Queries;
using BookingClone.Application.Features.Reservation.Responses;
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

        [HttpGet("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReservationById(int Id)
        {
            var query = new GetReservationByIdQuery() { Id = Id };

            Result<ReservationResponseDto> res = await mediator.Send(query);

            return Ok(res);
        }

        [HttpGet("all")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReservationsPage([FromQuery] GetReservationsPageQuery query)
        {
            var res = await mediator.Send(query);
            return Ok(res);
        }

        [HttpPost("add")]
        [Authorize] 
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand request)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            request.UserId = userId;

            var res = await mediator.Send(request);

            if(!res.Success)
                return BadRequest(res);

            return Created();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateReservation([FromBody] UpdateReservationCommand request)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            request.UserId = userId;

            var res = await mediator.Send(request);

            if (!res.Success)
                return BadRequest(res);

            return CreatedAtAction(nameof(GetReservationById), new {id = request.Id}, res);
        }

        [HttpPut("cancel/{Id:int}")]
        [Authorize]
        public async Task<IActionResult> CancelReservation([FromRoute] int Id)
        {
            var cmd = new DeleteReservationCommand() { Id = Id };

            await mediator.Send(cmd);

            return Ok("Reservation Canceled Successfully");
        }


        [HttpGet("my-reservations")]
        [Authorize]
        public async Task<IActionResult> GetMyReservations()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var res = await mediator.Send(userId);

            return Ok(res);
        }

    }
}
