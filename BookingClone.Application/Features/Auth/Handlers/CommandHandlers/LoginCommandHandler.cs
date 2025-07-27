
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

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResponseDto>>
{
    private readonly UserManager<User> userManager;
    private readonly IJwtService jwtService;
    private readonly ILogger<LoginCommandHandler> logger;

    public LoginCommandHandler(UserManager<User> userManager,
        IJwtService jwtService,
        ILogger<LoginCommandHandler> logger)
    {
        this.userManager = userManager;
        this.jwtService = jwtService;
        this.logger = logger;
    }

    public async Task<Result<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(request.Email);


        if (user == null)
            throw new LoginFailedException("Email or Password not correct");
        

        if (!await userManager.CheckPasswordAsync(user, request.Password))
            throw new LoginFailedException("Email or Password not correct");

        TokenResponseDto tokenResponseDto = await jwtService.GetTokensAsync(user.Id);

        return  Result<TokenResponseDto>.CreateSuccessResult(tokenResponseDto);
    }
}
