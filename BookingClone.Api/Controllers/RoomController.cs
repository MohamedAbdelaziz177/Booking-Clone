using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Commands;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("SlidingWindow")]
    public class RoomController : ControllerBase
    {
        private readonly IMediator mediator;

        public RoomController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get a specific room by its ID.
        /// </summary>
        /// <param name="Id">The ID of the room.</param>
        /// <returns>Room details.</returns>
        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoomById(int Id)
        {
            var Query = new GetRoomByIdQuery() { Id = Id };
            var res = await mediator.Send(Query);
            return Ok(res);
        }

        /// <summary>
        /// Get a paginated list of all rooms.
        /// </summary>
        /// <param name="Query">Pagination and filter options.</param>
        /// <returns>Paged room data.</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRooms([FromQuery] GetRoomPageQuery Query)
        {
            var res = await mediator.Send(Query);
            return Ok(res);
        }

        /// <summary>
        /// Get all rooms available between two dates.
        /// </summary>
        /// <param name="Query">Check-in and check-out date range.</param>
        /// <returns>List of available rooms.</returns>
        [HttpGet("get-available-between")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomsAvailableBetween([FromQuery] GetAllRoomsAvailableBetweenQuery Query)
        {
            var res = await mediator.Send(Query);
            return Ok(res);
        }

        /// <summary>
        /// Add a new room (Admin only).
        /// </summary>
        /// <param name="createRoomCommand">Room creation data.</param>
        /// <returns>Room created response.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddRoom(CreateRoomCommand createRoomCommand)
        {
            var res = await mediator.Send(createRoomCommand);
            return Created();
        }

        /// <summary>
        /// Add an image to a room (Admin only).
        /// </summary>
        /// <param name="request">Image upload data.</param>
        /// <returns>Success response.</returns>
        [HttpPost("image")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddRoomImage(AddRoomImageCommand request)
        {
            var res = await mediator.Send(request);
            return Ok(res);
        }

        /// <summary>
        /// Update room details (Admin only).
        /// </summary>
        /// <param name="updateRoomCommand">Room update data.</param>
        /// <returns>Updated room info.</returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateRoom(UpdateRoomCommand updateRoomCommand)
        {
            var res = await mediator.Send(updateRoomCommand);
            return CreatedAtAction(nameof(GetRoomById), new { id = res.Data!.Id }, res);
        }

        /// <summary>
        /// Delete a room by its ID (Admin only).
        /// </summary>
        /// <param name="id">Room ID.</param>
        /// <returns>Deletion result.</returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var cmd = new DeleteRoomCommand() { Id = id };
            var res = await mediator.Send(cmd);
            return Ok(res);
        }

        /// <summary>
        /// Remove a room image by its ID (Admin only).
        /// </summary>
        /// <param name="Id">Image ID.</param>
        /// <returns>Image deletion result.</returns>
        [HttpDelete("image/{Id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveRoomImage([FromRoute] int Id)
        {
            var cmd = new RemoveRoomImageCommand() { ImageId = Id };
            var res = await mediator.Send(cmd);
            return Ok(res);
        }
    }
}
