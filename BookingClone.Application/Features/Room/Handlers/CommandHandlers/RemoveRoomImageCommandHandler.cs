

using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Room.Handlers.CommandHandlers;

public class RemoveRoomImageCommandHandler : IRequestHandler<RemoveRoomImageCommand, Result<string>>
{
    private readonly IUnitOfWork unitOfWork;

    public RemoveRoomImageCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<Result<string>> Handle(RemoveRoomImageCommand request, CancellationToken cancellationToken)
    {
        RoomImage? img = await unitOfWork.RoomImageRepo.GetByIdAsync(request.ImageId);

        if (img == null)
            throw new EntityNotFoundException("Image does not exist");

        await unitOfWork.RoomImageRepo.DeleteAsync(img);

        return new Result<string>("Deleted Successfully");
    }
}
