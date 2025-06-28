

using BookingClone.Application.Common;
using MediatR;

namespace BookingClone.Application.Features.Auth.Commands;

public class RegisterCommand : IRequest<Result<string>>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;
}
