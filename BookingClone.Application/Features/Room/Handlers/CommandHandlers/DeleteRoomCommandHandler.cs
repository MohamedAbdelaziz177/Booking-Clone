

using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;
using RoomEntity = BookingClone.Domain.Entities.Room;

namespace BookingClone.Application.Features.Room.Handlers.CommandHandlers;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Result<string>>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result<string>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        RoomEntity? room = await unitOfWork.RoomRepo.GetByIdAsync(request.Id);

        if (room == null)
            throw new EntityNotFoundException("Room not found");

        await unitOfWork.RoomRepo.DeleteAsync(room);

        return new Result<string>("Deleted Successfully");

    }
}
