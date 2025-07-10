using BookingClone.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<string> AddRole(string roleName)
        {
            return null;
        }

        [HttpDelete]
        public async Task<string> RemoveRole(string roleName)
        {
            return null;
        }

        [HttpPut]
        public async Task<string> AssignRoleToUser(string userId, string roleName)
        {
            return null;
        }
    }
}
