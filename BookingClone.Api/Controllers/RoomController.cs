using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Commands;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMediator mediator;

        public RoomController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetRoomById(int Id)
        {
            var Query = new GetRoomByIdQuery() { Id = Id };
            var res = await mediator.Send(Query);

            return Ok(res);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRooms([FromQuery] GetRoomPageQuery Query)
        {
            var res = await mediator.Send(Query);
            return Ok(res);
        }

        [HttpGet("get-available-between")]
        public async Task<IActionResult> GetRoomsAvailableBetween([FromQuery] GetAllRoomsAvailableBetweenQuery Query)
        {
            var res = await mediator.Send(Query);
            return Ok(res);
        }


        [HttpPost]
        //[Authorize(Roles = "Admin")] 
        public async Task<IActionResult> AddRoom(CreateRoomCommand createRoomCommand)
        {
            var res = await mediator.Send(createRoomCommand);
            return Created();
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomCommand updateRoomCommand)
        {
            var res = await mediator.Send(updateRoomCommand);
            return CreatedAtAction(nameof(GetRoomById), new { id = res.Data!.Id }, res);
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var cmd = new DeleteRoomCommand() { Id = id };
            var res = await mediator.Send(cmd);

            return Ok(res);
        }

        [HttpPost("image")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoomImage(AddRoomImageCommand request)
        {
            var res = await mediator.Send(request);
            return Ok(res);
        }

        [HttpDelete("image/{Id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRoomImage([FromRoute] int Id)
        {
            var cmd = new RemoveRoomImageCommand() { ImageId = Id } ;
            var res = await mediator.Send(cmd);

            return Ok(res);
        }

    }
}
 