
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, Result<string>>
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AssignUserRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<Result<string>> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        var role = await roleManager.FindByNameAsync(request.RoleName);

        if (user == null || role == null) 
            throw new  EntityNotFoundException("Invalid userId or role");

        IdentityResult res = await userManager.AddToRoleAsync(user, request.RoleName);

        if (!res.Succeeded)
            throw new Exception("Assiging failed, try again");

        return Result<string>.CreateSuccessResult(data: "Role Assigned to User successfully");
    }
}
