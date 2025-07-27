using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Commands;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Reservation.Queries;
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
    public class HotelController : ControllerBase
    {
        private readonly IMediator mediator;

        public HotelController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get a specific hotel by its ID.
        /// </summary>
        /// <param name="Id">The ID of the hotel.</param>
        /// <returns>Returns the hotel details if found.</returns>
        /// <response code="200">Returns the hotel data</response>
        /// <response code="404">If the hotel is not found</response>
        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHotelById(int Id)
        {
            GetHotelByIdQuery query = new GetHotelByIdQuery() { id = Id };
            var res = await mediator.Send(query);
            return Ok(res);
        }

        /// <summary>
        /// Get a paginated list of all hotels.
        /// </summary>
        /// <param name="query">Pagination and filter options.</param>
        /// <returns>A paged list of hotels.</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllHotels([FromQuery] GetHotelPageQuery query)
        {
            var res = await mediator.Send(query);
            return Ok(res);
        }

        /// <summary>
        /// Get hotels located in a specific city.
        /// </summary>
        /// <param name="city">The name of the city.</param>
        /// <returns>List of hotels in the specified city.</returns>
        [HttpGet("get-by-city")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCity([FromQuery] string city)
        {
            var req = new GetHotelsByCityOrCountryQuery() { City = city };
            var res = await mediator.Send(req);
            return Ok(res);
        }

        /// <summary>
        /// Add a new hotel.
        /// </summary>
        /// <param name="createHotelCommand">Hotel creation request data.</param>
        /// <returns>Confirmation that the hotel was created.</returns>
        /// <response code="201">Hotel successfully created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddHotel(CreateHotelCommand createHotelCommand)
        {
            var res = await mediator.Send(createHotelCommand);
            return Created();
        }

        /// <summary>
        /// Update an existing hotel.
        /// </summary>
        /// <param name="updateHotelCommand">Hotel update data.</param>
        /// <returns>Returns the updated hotel data.</returns>
        /// <response code="200">Hotel successfully updated.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateHotel(UpdateHotelCommand updateHotelCommand)
        {
            var res = await mediator.Send(updateHotelCommand);
            return CreatedAtAction(nameof(GetHotelById), new { Id = res.Data!.Id }, res);
        }

        /// <summary>
        /// Delete a hotel by ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
        /// <returns>Confirmation that the hotel was deleted.</returns>
        /// <response code="200">Hotel successfully deleted.</response>
        /// <response code="404">If the hotel does not exist.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var cmd = new DeleteHotelCommand() { Id = id };
            var res = await mediator.Send(cmd);
            return Ok(res);
        }
    }
}
