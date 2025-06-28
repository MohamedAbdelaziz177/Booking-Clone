
using BookingClone.Application.Common;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Application.Features.Auth.Responses;
using MediatR;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenResponseDto>>
{
    public Task<Result<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
