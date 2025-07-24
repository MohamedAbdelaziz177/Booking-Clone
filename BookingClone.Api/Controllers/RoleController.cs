using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("SlidingWindow")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator mediator;

        public RoleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //[HttpPost]
        //public async Task<string> AddRole(string roleName)
        //{
        //    return null;
        //}
        //
        //[HttpDelete]
        //public async Task<string> RemoveRole(string roleName)
        //{
        //    return null;
        //}
        // مش وقته

        /// <summary>
        /// Assign a role to a user. Only accessible by Admins.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <param name="roleName">The role to assign (e.g., "User", "Admin").</param>
        /// <returns>Success message or error result.</returns>
        [HttpPut("assign-user-role")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AssignRoleToUser([FromQuery] string userId, [FromQuery] string roleName)
        {
            var req = new AssignUserRoleCommand
            {
                UserId = userId,
                RoleName = roleName
            };

            var res = await mediator.Send(req);

            return Ok(res);
        }
    }
}
