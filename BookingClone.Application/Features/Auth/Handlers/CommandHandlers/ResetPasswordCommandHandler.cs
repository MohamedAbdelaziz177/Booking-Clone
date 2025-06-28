
using BookingClone.Application.Common;
using BookingClone.Application.Features.Auth.Commands;
using MediatR;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
{
    public Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(); 
    }
}
