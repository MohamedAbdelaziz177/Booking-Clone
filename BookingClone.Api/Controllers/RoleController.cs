using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPut("assign-user-role")]
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleName)
        {
            var req = new AssignUserRoleCommand() { RoleName = roleName, UserId = userId };
            var res = await mediator.Send(req);
            return Ok(res);
        }
    }
}
