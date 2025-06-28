

using BookingClone.Application.Common;
using BookingClone.Application.Features.Auth.Responses;
using MediatR;

namespace BookingClone.Application.Features.Auth.Commands;

public class RefreshTokenCommand : IRequest<Result<TokenResponseDto>>
{
    public string RefreshToken { get; set; } = default!;
}
