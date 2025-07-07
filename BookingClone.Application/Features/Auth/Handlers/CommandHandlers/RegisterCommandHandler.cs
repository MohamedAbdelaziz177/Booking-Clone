
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IEmailService emailService;
    private readonly ILogger<RegisterCommandHandler> logger;
    private readonly UserManager<User> userManager;

    public RegisterCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IEmailService emailService,
        ILogger<RegisterCommandHandler> logger,
        UserManager<User> userManager)

    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.emailService = emailService;
        this.logger = logger;
        this.userManager = userManager;
    }

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user != null)
            throw new RegistrationFailedException("Email already exists");

        var AddedUser = request.Adapt<User>();
        //var AddedUser = new User() { Email = request.Email,
        //    Firstname = request.FirstName,
        //    Lastname = request.LastName,
        //    
        //};

        IdentityResult res =  await userManager.CreateAsync(AddedUser, request.Password);

        if(!res.Succeeded)
        {

            var errors = string.Join(" | ", res.Errors.Select(e => e.Description));
            logger.LogError(errors);

            throw new RegistrationFailedException(errors);
        }

        await userManager.AddToRoleAsync(user!, "User");
        //var AddedUser = await userManager.FindByEmailAsync(request.Email);

        var OTP = RandomNumberGenerator.GetInt32(10000, 100000); 

        if (AddedUser != null)
        {
            bool Sent = await emailService
                .SendMail(AddedUser.Email!,
                "Confirmation OTP",
                OTP.ToString()
                );

            if (Sent)
            {
                AddedUser.EmailConfirmationOtp = OTP;
                AddedUser.EmailConfirmationOtpExpiry = DateTime.Now.AddMinutes(5);
                await userManager.UpdateAsync(AddedUser);
            }
            else
            {
                logger.LogError("email not sent due to internal server error");
                throw new Exception("Email Not sent - try again");
            }
                
        }
             

        return new Result<string>(true, message: "Registeration successfully completed - check ur email");

    }
}
