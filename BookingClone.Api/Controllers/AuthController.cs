using BookingClone.Application.Common;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Application.Features.Auth.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace BookingClone.Api.Controllers
{
    /// <summary>
    /// Handles user authentication and authorization operations.
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("SlidingWindow")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHostEnvironment env;
        private readonly ILogger<AuthController> logger;

        
        public AuthController(IMediator mediator,
            IHostEnvironment Env,
            ILogger<AuthController> logger)
        {
            this.mediator = mediator;
            env = Env;
            this.logger = logger;
        }

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="registerCommand">The registration details (e.g. name, email, password).</param>
        /// <returns>A success message or error details.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            logger
                .LogInformation(registerCommand.FirstName + registerCommand.LastName + registerCommand.Email);
            Result<string> res = await mediator.Send(registerCommand);
            return Ok(res);
        }

        /// <summary>
        /// Authenticates a user and returns access/refresh tokens.
        /// </summary>
        /// <param name="loginCommand">The login credentials (email and password).</param>
        /// <returns>A JWT token and a refresh token (in cookie).</returns>

        [HttpPost("login")]
        [EnableRateLimiting("TokensBucket")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            Result<TokenResponseDto> res = await mediator.Send(loginCommand);
            SetRefTokenInCookie(res.Data!.RefreshToken);

            return Ok(res);
        }

        /// <summary>
        /// Issues a new access token using a valid refresh token stored in cookies.
        /// </summary>
        /// <returns>A new JWT access token.</returns>

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refToken = Request.Cookies["refresh-token"];

            if(refToken == null) 
                return Unauthorized("Refresh Token is missing");

            var refreshTokenCommand =
                new RefreshTokenCommand() { RefreshToken = refToken };

            Result<TokenResponseDto> res = await mediator.Send(refreshTokenCommand);

            SetRefTokenInCookie(res.Data!.RefreshToken);

            return Ok(res);
        }

        /// <summary>
        /// Resends the email confirmation code to the user's email.
        /// </summary>
        /// <param name="req">User's email.</param>
        /// <returns>A success message or error.</returns>
        [HttpPost("resend-confirmation-code")]
        [EnableRateLimiting("TokensBucket")]
        public async Task<IActionResult> ResendConfirmationCode([FromBody] ResendConfirmationCodeCommand req)
        {
            return Ok(await mediator.Send(req));
        }

        /// <summary>
        /// Confirms a user's email using the provided confirmation code.
        /// </summary>
        /// <param name="confirmEmailCommand">The email and confirmation code.</param>
        /// <returns>Success or failure of confirmation.</returns>
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmailCommand confirmEmailCommand)
        {
            return Ok(await mediator.Send(confirmEmailCommand));
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody]ForgetPasswordCommand req)
        {
            return Ok(await mediator.Send(req));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordCommand req)
        {
            return Ok(await mediator.Send(req));
        }

        /// <summary>
        /// Stores the refresh token in a secure HTTP-only cookie.
        /// </summary>
        /// <param name="refTok">The refresh token value.</param>
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

        /// <summary>
        /// Retrieves the refresh token from cookies.
        /// </summary>
        /// <returns>The refresh token or null.</returns>
        private string? GetRefreshTokenFromCookie()
        {
            return Request.Cookies["refresh-token"];
        }

       
    }
}
