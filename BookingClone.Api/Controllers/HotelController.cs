using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Commands;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Reservation.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator mediator;

        public HotelController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetHotelById(int Id)
        {
            GetHotelByIdQuery query = new GetHotelByIdQuery() { id = Id};
        
            var res = await mediator.Send(query);
        
            return Ok(res);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllHotels([FromQuery] GetHotelPageQuery query)
        {  
            var res = await mediator.Send(query);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddHotel(CreateHotelCommand createHotelCommand)
        {
            var res = await mediator.Send(createHotelCommand);
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHotel(UpdateHotelCommand updateHotelCommand)
        {
            var res = await mediator.Send(updateHotelCommand);
            return CreatedAtAction(nameof(GetHotelById), new  {Id = res.Data!.Id}, res);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var cmd = new DeleteHotelCommand() { Id = id };

            var res = await mediator.Send(cmd);
            return Ok(res);
        }

        [HttpGet("filter-by-city")]
        public async Task<IActionResult> GetByCity([FromQuery] string city)
        {
            var req = new GetHotelsByCityOrCountryQuery() { City = city };

            var res = await mediator.Send(req);

            return Ok(res);
        }
    }
}
