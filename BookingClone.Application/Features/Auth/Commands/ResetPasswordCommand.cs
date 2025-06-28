
using BookingClone.Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BookingClone.Application.Features.Auth.Commands;

public class ResetPasswordCommand : IRequest<Result<string>>
{
    public string email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}
