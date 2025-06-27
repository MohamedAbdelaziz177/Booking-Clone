
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Room.Handlers.CommandHandlers;

public class AddRoomImageCommandHandler : IRequestHandler<AddRoomImageCommand, Result<RoomResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;

    public AddRoomImageCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task<Result<RoomResponseDto>> Handle(AddRoomImageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
