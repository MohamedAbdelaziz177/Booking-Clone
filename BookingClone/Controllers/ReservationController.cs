using BookingClone.Application.Features.Reservation.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetReservationById(int Id)
        {
            return null;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetReservationsPage([FromQuery] int PageIdx,
            [FromQuery] string SortField = "Id",
            [FromQuery] string SortDir = "Desc")
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand request)
        {
            return null;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservation([FromBody] UpdateReservationCommand request)
        {
            return null;
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelReservation([FromRoute] int Id)
        {
            return null;
        }


    }
}
