

using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Application.Features.Auth.Responses;
using BookingClone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<TokenResponseDto>>
{
    private readonly UserManager<User> userManager;
    private readonly IJwtService jwtService;
    private readonly ILogger<ConfirmEmailCommandHandler> logger;

    public ConfirmEmailCommandHandler(UserManager<User> userManager,
        IJwtService jwtService,
        ILogger<ConfirmEmailCommandHandler> logger
        )
    {
        this.userManager = userManager;
        this.jwtService = jwtService;
        this.logger = logger;
    }

    public async Task<Result<TokenResponseDto>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new EntityNotFoundException("no such user existed");

        logger.LogInformation(user.EmailConfirmationOtp.ToString() + "---" + request.token
            + "---" + (request.token == user.EmailConfirmationOtp.ToString())
            + "---" + user.EmailConfirmationOtpExpiry.ToString());

        if (user.EmailConfirmationOtp.ToString() != request.token 
            || user.EmailConfirmationOtpExpiry < DateTime.UtcNow)
            throw new OtpNotValidException("Code is not valid");

        


        user.EmailConfirmed = true;
        user.EmailConfirmationOtp = 0;
        user.EmailConfirmationOtpExpiry = null;
        
        var IdentRes = await userManager.UpdateAsync(user);

        if (!IdentRes.Succeeded)
            throw new Exception("User Update Failed After Email Confirmation");

        TokenResponseDto tokenResponse = await jwtService.GetTokensAsync(user.Id);

        return Result<TokenResponseDto>.CreateSuccessResult(tokenResponse);

        
    }
}
