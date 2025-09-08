
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
{
    private readonly UserManager<User> userManager;
    public ResetPasswordCommandHandler(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(request.email);

        if (user == null)
            throw new EntityNotFoundException("No user associated to this id");

        if(user.PasswordResetOtp.ToString() != request.Token 
            || user.PasswordResetOtpExpiry < DateTime.UtcNow)
        {
            throw new OtpNotValidException("Code is not valid");
        }

        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, request.Password);

        user.PasswordResetOtp = 0;
        user.PasswordResetOtpExpiry = DateTime.UtcNow;

        await userManager.UpdateAsync(user);

        return Result<string>.CreateSuccessResult("New Passowrd Set Correctly");
    }
}
