

using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Application.Features.Auth.Responses;
using BookingClone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class ResendConfirmationCodeCommandHandler : IRequestHandler<ResendConfirmationCodeCommand , Result<string>>
{
    private readonly IEmailService emailService;
    private readonly UserManager<User> userManager;

    public ResendConfirmationCodeCommandHandler(IEmailService emailService,
        UserManager<User> userManager)
    {
        this.emailService = emailService;
        this.userManager = userManager;
    }
    public async Task<Result<string>> Handle(ResendConfirmationCodeCommand request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new EntityNotFoundException("No such user found");

        user.EmailConfirmationOtp = RandomNumberGenerator
            .GetInt32(MagicValues.MIN_OTP_VAL, MagicValues.MAX_OTP_VAL); // (!env.isProd)? 12345;

        user.EmailConfirmationOtpExpiry = DateTime.Now.AddMinutes(MagicValues.OTP_EXPIRY_MINS);

        await userManager.UpdateAsync(user);

        //string token = await userManager.GenerateEmailConfirmationTokenAsync(user); -- مش مستاهله وجع دماغ
        
        await emailService.SendMail(request.Email, "Confirmation Code",
            user.EmailConfirmationOtp.ToString());

        return new Result<string>(data:
            "email confirmation token sent successfully");
    }

     
}
