

using BookingClone.Application.Common;
using BookingClone.Application.Features.Auth.Commands;
using MediatR;

namespace BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, Result<string>>
{
    public Task<Result<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
