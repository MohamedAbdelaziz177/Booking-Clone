﻿using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Commands;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Queries;
using MediatR;
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

        [HttpGet]
        public async Task<IActionResult> GetAllRooms(int PageIdx = 0,
            string SortField = "Id",
            string SortDir = "Desc",
            string City = "All",
            bool Active = true
            )
        {
            var Query = new GetRoomPageQuery()
            {
                PageIdx = PageIdx,
                PageSize = MagicValues.PAGE_SIZE,
                SortField = SortField,
                SortType = Enum.Parse<SortType>(SortDir)
            };

            var res = await mediator.Send(Query);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(CreateRoomCommand createRoomCommand)
        {
            var res = await mediator.Send(createRoomCommand);
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom(UpdateRoomCommand updateRoomCommand)
        {
            var res = await mediator.Send(updateRoomCommand);
            return CreatedAtAction(nameof(GetRoomById), new { id = res.Data!.Id }, res);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var cmd = new DeleteRoomCommand() { Id = id };
            var res = await mediator.Send(cmd);

            return Ok(res);
        }
    }
}
 