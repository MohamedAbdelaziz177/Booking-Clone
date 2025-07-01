
using BookingClone.Application.Common;
using MediatR;

namespace BookingClone.Application.Features.Auth.Commands;

public class ResendConfirmationCodeCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = default!;
}
