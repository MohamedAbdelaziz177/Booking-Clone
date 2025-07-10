
using BookingClone.Application.Common;
using MediatR;

namespace BookingClone.Application.Features.Auth.Commands;

public class AssignUserRoleCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = default!;

    public string RoleName {  get; set; } = default!;
}
