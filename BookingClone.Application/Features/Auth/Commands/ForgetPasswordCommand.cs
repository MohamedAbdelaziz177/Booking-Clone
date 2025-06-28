

using BookingClone.Application.Common;
using MediatR;

namespace BookingClone.Application.Features.Auth.Commands;

public class ForgetPasswordCommand : IRequest<Result<string>>
{
    public string email { get; set; } = string.Empty;
}
