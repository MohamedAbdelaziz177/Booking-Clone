using BookingClone.Application.Common;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Application.Features.Auth.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHostEnvironment env;
        private readonly ILogger<AuthController> logger;

        [HttpGet("test")]
        public IActionResult test()
        {
            return Ok("kosom keda");
        }
        public AuthController(IMediator mediator,
            IHostEnvironment Env,
            ILogger<AuthController> logger)
        {
            this.mediator = mediator;
            env = Env;
            this.logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            logger
                .LogInformation(registerCommand.FirstName + registerCommand.LastName + registerCommand.Email);
            Result<string> res = await mediator.Send(registerCommand);
            return Ok(res);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            Result<TokenResponseDto> res = await mediator.Send(loginCommand);
            SetRefTokenInCookie(res.Data!.RefreshToken);

            return Ok(res);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refToken = Request.Cookies["refresh-token"];

            if(refToken == null) 
                return Unauthorized("Refresh Token is missing");

            var refreshTokenCommand =
                new RefreshTokenCommand() { RefreshToken = refToken };

            Result<TokenResponseDto> res = await mediator.Send(refreshTokenCommand);

            SetRefTokenInCookie(refToken);

            return Ok(res);
        }

        [HttpPost("resend-confirmation-code")]
        public async Task<IActionResult> ResendConfirmationCode([FromBody] ResendConfirmationCodeCommand req)
        {
            var res = await mediator.Send(req);
            return Ok(res);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmailCommand confirmEmailCommand)
        {
            var res = await mediator.Send(confirmEmailCommand);
            return Ok(res);
        }


        private void SetRefTokenInCookie(string refTok)
        {
            CookieOptions cookieOptions = new CookieOptions()
            {
                Secure = env.IsProduction(),
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(15),
                Path = "/api"
            };

            Response.Cookies.Append("refresh-token", refTok, cookieOptions);
        }


        private string? GetRefreshTokenFromCookie()
        {
            return Request.Cookies["refresh-token"];
        }

        // forget password / reset .. كلام فاضي 
    }
}
