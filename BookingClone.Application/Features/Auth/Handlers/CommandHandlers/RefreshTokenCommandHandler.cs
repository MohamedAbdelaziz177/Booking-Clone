
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Application.Features.Auth.Responses;
using MediatR;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
{
    private readonly IJwtService jwtService;

    public RefreshTokenCommandHandler(IJwtService jwtService)
    {
        this.jwtService = jwtService;
    }
    public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        TokenResponseDto responseDto = await jwtService.RefreshTokenAsync(request.RefreshToken);
        return new Result<TokenResponseDto>(responseDto);
    }
}
