

using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, Result<string>>
{
    private readonly UserManager<User> userManager;
    private readonly IEmailService emailService;

    public ForgetPasswordCommandHandler(UserManager<User> userManager,
        IEmailService emailService)
    {
        this.userManager = userManager;
        this.emailService = emailService;
    }
    public async Task<Result<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(request.email);

        if (user == null)
            throw new EntityNotFoundException("No user associated with this email");

        user.PasswordResetOtp = user.EmailConfirmationOtp = RandomNumberGenerator
            .GetInt32(MagicValues.MIN_OTP_VAL, MagicValues.MAX_OTP_VAL);

        user.PasswordResetOtpExpiry = DateTime.UtcNow.AddMinutes(MagicValues.OTP_EXPIRY_MINS);

        await userManager.UpdateAsync(user);

        await emailService.SendMail(request.email, "Password Reset OTP", user.PasswordResetOtp.ToString());

        return Result<string>.CreateSuccessResult("Code sent - Check ur email");
    }
}
