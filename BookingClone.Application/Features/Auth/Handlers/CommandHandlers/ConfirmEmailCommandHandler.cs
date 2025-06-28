

using BookingClone.Application.Common;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Application.Features.Auth.Responses;
using MediatR;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<TokenResponseDto>>
{
    public Task<Result<TokenResponseDto>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {

        throw new NotImplementedException();
    }
}
