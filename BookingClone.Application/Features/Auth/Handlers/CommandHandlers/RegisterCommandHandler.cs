
using AutoMapper;
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;

    public RegisterCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<User> userManager)

    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.userManager = userManager;
    }

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user != null)
            throw new RegistrationFailedException("Email already exists");

        IdentityResult res =  await userManager.CreateAsync(mapper.Map<User>(request), request.Password);

        if(!res.Succeeded)
        {
            // Send Confirmation Code

            string errors = "";

            foreach (var error in res.Errors)
                errors += error;

            throw new RegistrationFailedException(errors);
        }

        throw new NotImplementedException();
    }
}
